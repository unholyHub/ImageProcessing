// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="ZhZ Inc.">
//   Copyright (c) ZhZ Inc. All rights reserved.
// </copyright>
// <summary>
//   Interaction logic for MainWindow.xaml
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ImageProcessing
{
    using System.Diagnostics;
    using System.Drawing;
    using System.Text;
    using System.Windows;
    using ImageProcessingTechniques;

    /// <summary>
    /// Interaction logic for MainWindow.
    /// </summary>
    public partial class MainWindow
    {
        /// <summary>
        /// The directory path for the input image with high resolution.
        /// </summary>
        private const string ImagePathHigh = @"Images/Just Microsoft.jpg";

        /// <summary>
        /// The directory path for the input image with low resolution.
        /// </summary>
        private const string ImagePathLow = @"Images/Just Microsoft Stand.jpg";

        /// <summary>
        /// The stopwatch 1.
        /// </summary>
        private readonly Stopwatch stopwatch1 = new Stopwatch();

        /// <summary>
        /// The stopwatch 2.
        /// </summary>
        private readonly Stopwatch stopwatch2 = new Stopwatch();

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// The crop button click for cropping an image.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void CropBtnClick(object sender, RoutedEventArgs e)
        {
            const string CloneCropOutput = @"Images/Crop/Microsoft Clone Crop.jpg";
            const string RectangleCloneCropOutput = @"Images/Crop/Microsoft Rectangle Crop.jpg";
            this.stopwatch1.Start();
                Image cloneCropBmp = Crop.CloneCrop(new Bitmap(ImagePathLow), 450, 733, 100, 1500);
                this.stopwatch1.Stop();

                this.stopwatch2.Start();
                Image rectangleCloneCrop = Crop.RectangleCrop(new Bitmap(ImagePathLow), 450, 733, 100, 1500);
                this.stopwatch2.Stop();

                cloneCropBmp.Save(CloneCropOutput);
                rectangleCloneCrop.Save(RectangleCloneCropOutput);
            
            MessageBox.Show(
                string.Format(
                    "Clone crop: {0} ms.\n Rectangle crop: {1} ms.",
                    this.stopwatch1.ElapsedMilliseconds,
                    this.stopwatch2.ElapsedMilliseconds));
        }

        /// <summary>
        /// The rotate button click for rotating an image.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void RotateBtnClick(object sender, RoutedEventArgs e)
        {
            const string MathRotateOutput = @"Images/Rotate/Microsoft Math Rotate.jpg";
            const string MathOptsRotateOutput = @"Images/Rotate/Microsoft Math Opts Rotate.jpg";

            float rotationAngle;

            if (!float.TryParse(this.RotationAngleTextBox.Text, out rotationAngle))
            {
                MessageBox.Show("Invalid angle");
            }

            this.stopwatch1.Start();
            Image mathRotate = Rotate.GraphicsMathRotate(new Bitmap(ImagePathLow), rotationAngle);
            this.stopwatch1.Stop();

            this.stopwatch2.Start();
            Image mathOptsRotate = Rotate.GraphicsOptsMathRotate(new Bitmap(ImagePathLow), rotationAngle);
            this.stopwatch2.Stop();

            mathRotate.Save(MathRotateOutput);
            mathOptsRotate.Save(MathOptsRotateOutput);
            
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Average time for methods:");
            sb.AppendLine(string.Format("Math rotate: {0} ms.", this.stopwatch1.ElapsedMilliseconds));
            sb.AppendLine(string.Format("Math opts rotate: {0} ms.", this.stopwatch2.ElapsedMilliseconds));

            MessageBox.Show(sb.ToString());
        }

        /// <summary>
        /// The filter button click for filtering an image.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void FilterBtnClick(object sender, RoutedEventArgs e)
        {
            const string GetSetOutput = @"Images/Filtration/Microsoft Get Set Pixel Filtration.jpg";
            const string LockBitsFilter = @"Images/Filtration/Microsoft Lock Bits Filtration.jpg";

            byte filterValue = byte.Parse(this.FilterValueTextBox.Text);
            
            this.stopwatch1.Start();
            Image getSetPixelFilter = Filtration.GetSetPixelFiltration(new Bitmap(ImagePathLow), filterValue);
            this.stopwatch1.Stop();
            
            this.stopwatch2.Start();
            Image lockBitsFilter = Filtration.LockBitsFiltration(new Bitmap(ImagePathLow), filterValue);
            this.stopwatch2.Stop();
            
            getSetPixelFilter.Save(GetSetOutput);
            lockBitsFilter.Save(LockBitsFilter);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Average time for methods:");
            sb.AppendLine(string.Format("Get set pixel filter: {0} ms.", this.stopwatch1.ElapsedMilliseconds));
            sb.AppendLine(string.Format("Lock bits filter: {0} ms.", this.stopwatch2.ElapsedMilliseconds));

            MessageBox.Show(sb.ToString());
        }
    }
}
