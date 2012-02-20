using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Microsoft.Research.Kinect.Nui;
using Vector = Microsoft.Research.Kinect.Nui.Vector;

namespace StarfighterK
{


    public class Player
    {
        public bool isAlive;
        public DateTime lastUpdated;
        private int id;
        private SkeletonData skeleton;

        public Player(SkeletonData skeletonData)
        {
            skeleton = skeletonData;
            lastUpdated = DateTime.Now;
        }

        public int getId()
        {
            return id;
        }

        //void UpdateSegmentPosition(JointID j1, JointID j2, PlayerUtils.Segment seg)
        //{
        //    var bone = new PlayerUtils.Bone(j1, j2);
        //    if (segments.ContainsKey(bone))
        //    {
        //        PlayerUtils.BoneData data = segments[bone];
        //        data.UpdateSegment(seg);
        //        segments[bone] = data;
        //    }
        //    else
        //        segments.Add(bone, new PlayerUtils.BoneData(seg));
        //}

        //public void UpdateBonePosition(Microsoft.Research.Kinect.Nui.JointsCollection joints, JointID j1, JointID j2)
        //{
        //    var seg = new PlayerUtils.Segment(joints[j1].Position.X * playerScale + playerCenter.X,
        //                          playerCenter.Y - joints[j1].Position.Y * playerScale,
        //                          joints[j2].Position.X * playerScale + playerCenter.X,
        //                          playerCenter.Y - joints[j2].Position.Y * playerScale);
        //    seg.radius = Math.Max(3.0, playerBounds.Height * BONE_SIZE) / 2;
        //    UpdateSegmentPosition(j1, j2, seg);
        //}

        public void UpdateJointPosition(SkeletonData data)
        {
            this.skeleton = data;
            //var seg = new PlayerUtils.Segment(joints[j].Position.X * playerScale + playerCenter.X,
            //                      playerCenter.Y - joints[j].Position.Y * playerScale);

            //seg.radius = playerBounds.Height * ((j == JointID.Head) ? HEAD_SIZE : HAND_SIZE) / 2;
            //UpdateSegmentPosition(j, j, seg);
        }

        public Vector GetJointPosition(JointID joint)
        {
            return skeleton.Joints[joint].Position;
        }

        //public void Draw(UIElementCollection children)
        //{
        //    if (!isAlive)
        //        return;

        //    DateTime cur = DateTime.Now;

        //    foreach (var segment in segments)
        //    {
        //        PlayerUtils.Segment seg = segment.Value.GetEstimatedSegment(cur);
        //        if (seg.IsCircle())
        //        {
        //            var circle = new Ellipse();
        //            circle.Width = seg.radius * 2;
        //            circle.Height = seg.radius * 2;
        //            circle.SetValue(Canvas.LeftProperty, seg.x1 - seg.radius);
        //            circle.SetValue(Canvas.TopProperty, seg.y1 - seg.radius);
        //            circle.Stroke = brJoints;
        //            circle.StrokeThickness = 1;
        //            circle.Fill = brBones;
        //            children.Add(circle);
        //        }
        //    }

        //    // Remove unused players after 1/2 second.
        //    if (DateTime.Now.Subtract(lastUpdated).TotalMilliseconds > 500)
        //        isAlive = false;
        //}
    }
}

