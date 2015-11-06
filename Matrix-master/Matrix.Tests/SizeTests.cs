using NUnit.Framework;

namespace Matrix.Tests
{

    public class Size
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public bool IsSquare { get; set; }

        public Size(int width, int height)
        {
            Width = width;
            Height = height;
            IsSquare = (Height == Width) ? true : false;
        }

        public override bool Equals(object obj)
        {
            Size s = obj as Size;
            if (obj == null || obj.GetType() != this.GetType())
            {
                return false;
            }
            return  Width == s.Width && Height == s.Height;
        }

        public bool Equals(Size s)
        {
            if (s == null)
            {
                return false;
            }
            return Width == s.Width && Height == s.Height;
        }

        public override int GetHashCode()
        {
            return Width.GetHashCode() ^ Height.GetHashCode();
        }
        
        public static bool operator ==(Size a, Size b)
        {
            if (object.ReferenceEquals(a, b))
            {
                return true;
            }

            return a.Width == b.Width && a.Height == b.Height;
        }

        public static bool operator !=(Size a, Size b)
        {
            return !(a == b);
        }
    }

    public class SizeTests
    {
        [Test]
        public void WidthProperty_Always_ReturnsWidth()
        {
            var size = new Size(width: 1, height: 2);

            Assert.AreEqual(1, size.Width);
        }

        [Test]
        public void HeightProperty_Always_ReturnsHeight()
        {
            var size = new Size(width: 1, height: 2);

            Assert.AreEqual(2, size.Height);
        }

        [Test]
        public void IsSquare_WhenHeightIsTheSameAsWidth_ReturnsTrue()
        {
            var size = new Size(width: 1, height: 1);

            Assert.IsTrue(size.IsSquare);
        }

        [Test]
        public void IsSquare_WhenHeightAndWidthAreDifferent_ReturnsFalse()
        {
            var size = new Size(width: 1, height: 2);

            Assert.IsFalse(size.IsSquare);
        }

        [Test]
        public void EqualsOperator_WhenHeightIsTheSameAsWidth_ReturnsTrue()
        {
            var sizeA = new Size(1, 1);
            var sizeB = new Size(1, 1);

            Assert.IsTrue(sizeA == sizeB);
        }

        [Test]
        public void EqualsOperator_WhenHeightAndWidthAreDifferent_ReturnsFalse()
        {
            var sizeA = new Size(1, 1);
            var sizeB = new Size(1, 2);
            var sizeC = new Size(2, 1);

            Assert.IsFalse(sizeA == sizeB);
            Assert.IsFalse(sizeA == sizeC);
            Assert.IsFalse(sizeB == sizeC);
        }
    }
}
