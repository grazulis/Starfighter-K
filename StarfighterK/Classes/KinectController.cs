using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Research.Kinect.Nui;

namespace StarfighterK
{
    public class KinectController : BaseFighterController, IFighterController
    {
        Runtime nui = new Runtime();
        private bool nuiInitialized = false;
        public Dictionary<int, Player> players = new Dictionary<int, Player>();

        public KinectController()
        {
            if ((nui != null) && InitializeNui())
            {
                nui.VideoFrameReady += new EventHandler<ImageFrameReadyEventArgs>(nui_ColorFrameReady);
                nui.SkeletonFrameReady += new EventHandler<SkeletonFrameReadyEventArgs>(nui_SkeletonFrameReady);
                //InitialiseSpeechRecognition();
            }
        }

        private bool InitializeNui()
        {
            UninitializeNui();
            if (nui == null)
                return false;
            try
            {
                nui.Initialize(RuntimeOptions.UseDepthAndPlayerIndex | RuntimeOptions.UseSkeletalTracking | RuntimeOptions.UseColor);
            }
            catch (Exception _Exception)
            {
                Console.WriteLine(_Exception.ToString());
                return false;
            }

            nui.DepthStream.Open(ImageStreamType.Depth, 2, ImageResolution.Resolution320x240, ImageType.DepthAndPlayerIndex);
            nui.VideoStream.Open(ImageStreamType.Video, 2, ImageResolution.Resolution640x480, ImageType.Color);
            nui.SkeletonEngine.TransformSmooth = true;
            nuiInitialized = true;
            return true;
        }

        private void UninitializeNui()
        {
            //TODO
            if ((nui != null) && (nuiInitialized))
                nui.Uninitialize();
            nuiInitialized = false;
        }

        void nui_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            //TODO Pick this apart a bit
            SkeletonFrame skeletonFrame = e.SkeletonFrame;

            int iSkeletonSlot = 0;

            foreach (SkeletonData data in skeletonFrame.Skeletons)
            {
                if (SkeletonTrackingState.Tracked == data.TrackingState)
                {
                    Player player;
                    if (players.ContainsKey(iSkeletonSlot))
                    {
                        player = players[iSkeletonSlot];
                        player.UpdateJointPosition(data);
                    }
                    else
                    {
                        player = new Player(data);
                        players.Add(iSkeletonSlot, player);
                    }

                    player.lastUpdated = DateTime.Now;

                }
                iSkeletonSlot++;
            }
        }

        public Image video = new Image();
        void nui_ColorFrameReady(object sender, ImageFrameReadyEventArgs e)
        {
            //Draws video from camera
            // 32-bit per pixel, RGBA image
            PlanarImage Image = e.ImageFrame.Image;
            video.Source = BitmapSource.Create(
                Image.Width, Image.Height, 96, 96, PixelFormats.Bgr32, null, Image.Bits, Image.Width * Image.BytesPerPixel);
        }


        void CheckPlayers()
        {
            foreach (var player in players.Where(player => !player.Value.isAlive))
            {
                // Player left scene since we aren't tracking it anymore, so remove from dictionary
                players.Remove(player.Value.getId());
                break;
            }
        }

        public void CloseController()
        {
            UninitializeNui();   
        }

        public Vector left = new Vector();
        public Vector right = new Vector();
        public void CheckInput(Starfighter starfighter)
        {
            CheckPlayers();
            if (players.Count > 0)
            {

                foreach (var player in players)
                {
                   
                    left = player.Value.GetJointPosition(JointID.HandLeft);
                    right = player.Value.GetJointPosition(JointID.HandRight);
                }


                if (left.Y > right.Y - 0.15 ) MoveRight(starfighter);
                if (left.Y < right.Y + 0.15 ) MoveLeft(starfighter);


            }
        }

    }
}