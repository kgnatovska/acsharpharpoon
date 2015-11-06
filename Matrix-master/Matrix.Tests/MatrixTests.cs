using System;
using System.Globalization;
using System.Net.NetworkInformation;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace Matrix.Tests
{
    public class CoolMatrix
    {
        public CoolMatrix(int[,] arr)
        {
            if (arr == null)
            {
                throw new ArgumentNullException();
            }
            int x = arr.GetLength(0);
            int y = arr.GetLength(1);
            Size = new Size(x, y);
            ArrayProperty = arr;
        }
        
        public Size Size;
        public int[,] ArrayProperty { get; set; }

        public bool CompareArrays(CoolMatrix matrix)
        {
            if (ArrayProperty.GetLength(0) != matrix.ArrayProperty.GetLength(0) ||
                ArrayProperty.GetLength(1) != matrix.ArrayProperty.GetLength(1))
                return false;
            for (int i = 0; i < ArrayProperty.GetLength(0); i++)
                for (int j = 0; j < ArrayProperty.GetLength(1); j++)
                    if (!this[i, j].Equals(matrix[i, j]))
                        return false;
            return true;
        }

        public bool IsSquare
        {
           get { return Size.IsSquare; } 
        }

        public static implicit operator CoolMatrix(int[,] array)
        {
            CoolMatrix matrix = new CoolMatrix(array);
            return matrix;
        }

        public override string ToString()
        {
            var printed = new StringBuilder();
            for (int i = 0; i < ArrayProperty.GetLength(0); i++)
            {
                if (i > 0)
                    printed.AppendLine();
                var line = new string[ArrayProperty.GetLength(0)];
                for (int j = 0; j < ArrayProperty.GetLength(1); j++)
                {
                    line[j] = ArrayProperty[i, j].ToString();
                }
                printed.AppendFormat("[{0}]", String.Join(", ", line));
            }
            return printed.ToString();
        }

        public int this[int indexX, int indexY]
        {
            get { return ArrayProperty[indexX, indexY]; }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != this.GetType())
            {
                return false;
            }
            return this.CompareArrays((CoolMatrix) obj);
        }

        public bool Equals(CoolMatrix matrix)
        {
            if (matrix == null)
                return false;
            return this.CompareArrays(matrix);
        }

        public static bool operator ==(CoolMatrix a, CoolMatrix b)
        {
            if (object.ReferenceEquals(a, b))
            {
                return true;
            }
            return a.CompareArrays(b);
        }
        
        public static bool operator !=(CoolMatrix a, CoolMatrix b)
        {
            return !(a == b);
        }

        public static CoolMatrix operator *(CoolMatrix matrix, int scalar)
        {
            for (int i = 0; i < matrix.ArrayProperty.GetLength(0); i++)
                for (int j = 0; j < matrix.ArrayProperty.GetLength(1); j++)
                    matrix.ArrayProperty[i, j] *= scalar;
            return matrix;
        }

        public static CoolMatrix operator +(CoolMatrix a, CoolMatrix b)
        {
            if (a.Size != b.Size)
                throw new ArgumentException("Cannot add matrices of different sizes");
            for (int i = 0; i < a.ArrayProperty.GetLength(0); i++)
                for (int j = 0; j < a.ArrayProperty.GetLength(1); j++)
                    a.ArrayProperty[i, j] += b.ArrayProperty[i, j];
            return a;
        }

        public CoolMatrix Transpose()
        {
            var new_height = Size.Width;
            var new_width = Size.Height;
            Size = new Size(new_width, new_height);
            var new_array = new int [new_width, new_height];
            for (int i = 0; i < new_array.GetLength(0); i++)
                for (int j = 0; j < new_array.GetLength(1); j++)
                    new_array[i, j] = ArrayProperty[j, i];
            ArrayProperty = new_array;
            return this;
        }
    }

    public class MatrixTests
    {
        [Test]
        public void Constructor_WhenPassedArray_ReturnsCorrectSize()
        {
            var arr = new int[3, 2];
            var matrix = new CoolMatrix(arr);

            var expectedSize = new Size(width: 3, height:2);

            Assert.AreEqual(expectedSize, matrix.Size);
        }

        [Test]
        public void Constructor_WhenPassedNull_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new CoolMatrix(null);
            });
        }

        [Test]
        public void IsSquare_WithNotSquare_ReturnsFalse()
        {
            CoolMatrix matrix = new int [3,2];
            Assert.IsFalse(matrix.IsSquare);
        }

        [Test]
        public void IsSquare_WithSquare_ReturnsTrue()
        {
            CoolMatrix matrix = new int[2, 2];
            Assert.IsTrue(matrix.IsSquare);
        }

        [Test]
        public void ToString_Always_PrintsOutUnderlyingMatrix()
        {
            CoolMatrix matrix = new[,]
            {
                { 1, 2 },
                { 3, 4 }
            };

            var expected = $@"[1, 2]{Environment.NewLine}[3, 4]";

            Assert.AreEqual(expected, matrix.ToString());
        }

        [Test]
        public void Indexer_WhenUsed_ReturnsDataFromMatrix()
        {
            CoolMatrix matrix = new [,]
            {
                { 1, 2 },
                { 3, 4 }
            };

            Assert.AreEqual(1, matrix[0, 0]);
            Assert.AreEqual(2, matrix[0, 1]);
            Assert.AreEqual(3, matrix[1, 0]);
            Assert.AreEqual(4, matrix[1, 1]);
        }

        [Test]
        public void Indexer_UsingValueOutOfMatrixRange_ThrowsArgumentOutOfRangeException()
        {
            CoolMatrix matrix = new[,]
            {
                { 1, 2 },
                { 3, 4 }
            };

            Assert.Throws<IndexOutOfRangeException>(() =>
            {
                var temp = matrix[2, 0];
            });
        }

        [Test]
        public void EqalityOperator_WhenAllMembersAreEqual_ReturnsTrue()
        {
            CoolMatrix matrixA = new[,]
            {
                { 1, 2 },
                { 3, 4 }
            };

            CoolMatrix matrixB = new[,]
            {
                { 1, 2 },
                { 3, 4 }
            };

            Assert.IsTrue(matrixA == matrixB);
        }

        [Test]
        public void EqualityOperator_WhenAnyMemberIsNotEqual_ReturnsFalse()
        {
            CoolMatrix matrixA = new[,]
            {
                { 0, 2 },
                { 3, 4 }
            };

            CoolMatrix matrixB = new[,]
            {
                { 1, 2 },
                { 3, 4 }
            };

            Assert.IsFalse(matrixA == matrixB);
        }

        [Test]
        public void MultiplyOperator_WithScalar_MultipliesEachElementByScalar()
        {
            CoolMatrix matrixA = new[,]
            {
                { 1, 2 },
                { 3, 4 }
            };

            CoolMatrix expected = new[,]
            {
                { 2, 4 },
                { 6, 8 }
            };

            var result = matrixA * 2;

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void AdditionOperator_AmongMatricies_AddsCorrespondingEntries()
        {
            CoolMatrix matrixA = new[,]
            {
                { 1, 2 },
                { 3, 4 }
            };

            CoolMatrix matrixB = new[,]
            {
                { 5, 6 },
                { 7, 8 }
            };

            CoolMatrix expected = new[,]
            {
                { 6, 8 },
                { 10, 12 }
            };
            
            var result = matrixA + matrixB;

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void AddOperator_WithMatrixesOfDifferentSize_ThrowsArgumentException()
        {
            CoolMatrix matrixA = new[,]
            {
                { 1, 2 },
                { 3, 4 }
            };

            CoolMatrix matrixB = new[,]
            {
                { 5, 6 }
            };

            Assert.Throws<ArgumentException>(() =>
            {
                var result = matrixA + matrixB;
            });
        }

        [Test]
        public void Transpose_Always_TransposesMatrix()
        {
            CoolMatrix matrix = new[,]
            {
                { 1, 2 }
            };

            CoolMatrix expected = new[,]
            {
                { 1 },
                { 2 }
            };

            Assert.AreEqual(expected, matrix.Transpose());
        }
    }
}
