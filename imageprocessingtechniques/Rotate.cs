// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Rotate.cs" company="Zhz Inc.">
//   Copyright (c) ZhZ Inc. All rights reserved.
// </copyright>
// <summary>
//   Defines the Rotate class.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ImageProcessingTechniques
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;

    /// <summary>
    /// The rotate class for an image processing.
    /// </summary>
    public static class Rotate
    {
        /// <summary>
        /// The rotation with graphics object using mathematical operations.
        /// </summary>
        /// <param name="img">
        /// The input image.
        /// </param>
        /// <param name="rotationAngle">
        /// The rotation angle.
        /// </param>
        /// <returns>
        /// The <see cref="Bitmap"/> that is rotated.
        /// </returns>
        public static Bitmap GraphicsMathRotate(Image img, float rotationAngle)
        {
            int oldWidth = img.Width;
            int oldHeight = img.Height;

            double angle = (rotationAngle * Math.PI) / 180; //// Converting the rotation angle to radians.

            //// http://i.stack.imgur.com/vFrh5.png
            double cos = Math.Abs(Math.Cos(angle));
            double sin = Math.Abs(Math.Sin(angle));

            int newWidth = (int)((oldWidth * cos) + (oldHeight * sin));
            int newHeight = (int)((oldWidth * sin) + (oldHeight * cos));

            Bitmap returnBitmap = new Bitmap(newWidth, newHeight);

            returnBitmap.SetResolution(img.HorizontalResolution, img.VerticalResolution);

            using (Graphics gfx = Graphics.FromImage(returnBitmap))
            {
                ////now we set the rotation point to the center of our image
                gfx.TranslateTransform((newWidth - oldWidth) / 2f, (newHeight - oldHeight) / 2f);

                gfx.TranslateTransform(img.Width / 2f, img.Height / 2f);

                ////now rotate the image
                gfx.RotateTransform(rotationAngle);
                
                ////move the image back
                gfx.TranslateTransform(-(img.Width / 2f), -(img.Height / 2f));

                ////set the InterpolationMode to HighQualityBicubic so to ensure a high
                ////quality image once it is transformed to the specified size
                gfx.InterpolationMode = InterpolationMode.HighQualityBicubic;

                gfx.DrawImage(img, new Point(0, 0));
            }

            return returnBitmap;
        }

        /// <summary>
        /// The rotation with graphics object using mathematical operations. Added additional options. 
        /// </summary>
        /// <param name="inputImage">
        /// The input image.
        /// </param>
        /// <param name="angleDegrees">
        /// The angle degrees.
        /// </param>
        /// <returns>
        /// The <see cref="Bitmap"/> that is rotated.
        /// </returns>
        public static Bitmap GraphicsOptsMathRotate(Image inputImage, float angleDegrees)
        {
            // Test for zero rotation and return a clone of the input image
            if (angleDegrees == 0f)
            {
                return inputImage.Clone() as Bitmap;
            }

            int oldWidth = inputImage.Width;
            int oldHeight = inputImage.Height;

            double angleRadians = (angleDegrees * Math.PI) / 180d;

            double cos = Math.Abs(Math.Cos(angleRadians));
            double sin = Math.Abs(Math.Sin(angleRadians));

            int newWidth = (int)Math.Round((oldWidth * cos) + (oldHeight * sin));
            int newHeight = (int)Math.Round((oldWidth * sin) + (oldHeight * cos));

            Bitmap newBitmap = new Bitmap(newWidth, newHeight, PixelFormat.Format32bppPArgb);
  
            newBitmap.SetResolution(inputImage.HorizontalResolution, inputImage.VerticalResolution);

            using (Graphics gfx = Graphics.FromImage(newBitmap))
            {
                gfx.CompositingMode = CompositingMode.SourceOver;
                gfx.CompositingQuality = CompositingQuality.HighSpeed;
                gfx.InterpolationMode = InterpolationMode.NearestNeighbor;
                gfx.PixelOffsetMode = PixelOffsetMode.HighSpeed;
                gfx.SmoothingMode = SmoothingMode.HighSpeed;

                // Set up the built-in transformation matrix to do the rotation and maybe scaling
                gfx.TranslateTransform(newWidth / 2f, newHeight / 2f);
                
                gfx.RotateTransform(angleDegrees);

                gfx.TranslateTransform((-oldWidth) / 2f, (-oldHeight) / 2f);

                gfx.DrawImage(inputImage, 0, 0);
            }

            return newBitmap;
        }
    }
}
