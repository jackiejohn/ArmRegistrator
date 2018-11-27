using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZXing;
using ZXing.Client.Result;
using ZXing.Common;
using ZXing.Rendering;


namespace ArmRegistrator
{
    public partial class FormBar : Form
    {
        private WebCam _wCam;
        private Timer _webCamTimer;
        private readonly BarcodeReader _barcodeReader=new BarcodeReader(){
                //AutoRotate = true,
                //TryInverted = false,
                Options = new DecodingOptions { TryHarder = true,PureBarcode = false,PossibleFormats=new List<BarcodeFormat>(){BarcodeFormat.QR_CODE}}
            };
        private readonly IList<ResultPoint> _resultPoints;
        private readonly IList<Result> _lastResults;
        private EncodingOptions EncodingOptions { get; set; }
        private Type Renderer { get; set; }
        private bool TryMultipleBarcodes { get; set; }
        private bool TryOnlyMultipleQRCodes { get; set; }


        public FormBar()
        {
            InitializeComponent();
            //barcodeReader = new BarcodeReader
            //{
            //    AutoRotate = true,
            //    TryInverted = true,
            //    Options = new DecodingOptions { TryHarder = true }
            //};
            //barcodeReader.ResultPointFound += point =>
            //{
            //    if (point == null)
            //        resultPoints.Clear();
            //    else
            //        resultPoints.Add(point);
            //};
            //barcodeReader.ResultFound += result =>
            //{
            //    txtType.Text = result.BarcodeFormat.ToString();
            //    txtContent.Text += result.Text + Environment.NewLine;
            //    lastResults.Add(result);
            //    var parsedResult = ResultParser.parseResult(result);
            //    if (parsedResult != null)
            //    {
            //        btnExtendedResult.Visible = !(parsedResult is TextParsedResult);
            //        txtContent.Text += "\r\n\r\nParsed result:\r\n" + parsedResult.DisplayResult + Environment.NewLine + Environment.NewLine;
            //    }
            //    else
            //    {
            //        btnExtendedResult.Visible = false;
            //    }
            //};
            //resultPoints = new List<ResultPoint>();
            //lastResults = new List<Result>();
            //Renderer = typeof(BitmapRenderer);

        }
        
        private void BtnDecode_Click(object sender, EventArgs e)
        {
            if (_wCam == null)
            {
                _wCam = new WebCam { Container = picWebCam };
                _wCam.OpenConnection();
                _webCamTimer = new Timer();
                _webCamTimer.Tick += webCamTimer_Tick;
                _webCamTimer.Interval = 120;
                _webCamTimer.Start();
            }
            else
            {
                _webCamTimer.Stop();
                _webCamTimer = null;
                _wCam.Dispose();
                _wCam = null;
            }
        }

        void webCamTimer_Tick(object sender, EventArgs e)
        {
            var bitmap = _wCam.GetCurrentImage();
            if (bitmap == null) return;
            var result = _barcodeReader.Decode(bitmap);
            if (result != null)
            {
                txtTypeWebCam.Text = result.BarcodeFormat.ToString();
                txtContentWebCam.Text = result.Text;
            }
        }
    }
}
