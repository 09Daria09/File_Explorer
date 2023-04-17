﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace File_Explorer
{
    public partial class Form1 : Form
    {
            ImageList image_list1 = new ImageList();
            ImageList image_list2 = new ImageList();
        public Form1()
        {
            InitializeComponent();

            image_list1.ColorDepth = ColorDepth.Depth32Bit;

            image_list1.ImageSize = new Size(16, 16);

            listView1.SmallImageList = image_list1;

            image_list2.ColorDepth = ColorDepth.Depth32Bit;

            image_list2.ImageSize = new Size(32, 32);

            listView1.LargeImageList = image_list2;

            string[] drives = System.IO.Directory.GetLogicalDrives();

            foreach (string drive in drives)
            {
                TreeNode driveNode = new TreeNode(drive);
                driveNode.Tag = drive;
                driveNode.Nodes.Add(new TreeNode()); 

                treeView1.Nodes.Add(driveNode);
            }
        }

        private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            TreeNode expandingNode = e.Node;

            if (expandingNode.Nodes.Count == 1 && expandingNode.Nodes[0].Text == "")
            {
                expandingNode.Nodes.Clear();
                string[] files = System.IO.Directory.GetFiles(expandingNode.Tag.ToString());
                string[] directories = System.IO.Directory.GetDirectories(expandingNode.Tag.ToString());
                int index = 1;
                foreach (string file in files)
                {
                    Icon icon = Icon.ExtractAssociatedIcon(file);
                    image_list1.Images.Add(icon);
                    image_list2.Images.Add(icon);
                    listView1.Items.Add(file, index++);
                    TreeNode fileNode = new TreeNode(System.IO.Path.GetFileName(file));
                    fileNode.Tag = file;
                    expandingNode.Nodes.Add(fileNode);
                }

                foreach (string directory in directories)
                {
                    TreeNode directoryNode = new TreeNode(System.IO.Path.GetFileName(directory));
                    directoryNode.Tag = directory;
                    directoryNode.Nodes.Add(new TreeNode()); 
                    expandingNode.Nodes.Add(directoryNode);
                }
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode selectedNode = e.Node;

            if (selectedNode.Nodes.Count == 1 && selectedNode.Nodes[0].Text == "")
            {
                selectedNode.Nodes.Clear();

                string[] files = System.IO.Directory.GetFiles(selectedNode.Tag.ToString());
                string[] directories = System.IO.Directory.GetDirectories(selectedNode.Tag.ToString());
                int index = 1;
                foreach (string file in files)
                {
                    Icon icon = Icon.ExtractAssociatedIcon(file);
                    image_list1.Images.Add(icon);
                    image_list2.Images.Add(icon);
                    listView1.Items.Add(file, index++);
                    TreeNode fileNode = new TreeNode(System.IO.Path.GetFileName(file));
                    fileNode.Tag = file;
                    selectedNode.Nodes.Add(fileNode);
                }

                foreach (string directory in directories)
                {
                    TreeNode directoryNode = new TreeNode(System.IO.Path.GetFileName(directory));
                    directoryNode.Tag = directory;
                    directoryNode.Nodes.Add(new TreeNode()); 
                    selectedNode.Nodes.Add(directoryNode);
                }
            }
        }

        private void списокToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.View = View.Details;
        }

        private void таблицаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.View = View.Tile;
        }

        private void плиткаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.View = View.SmallIcon;
        }

        private void большиеЗначкиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listView1.View = View.LargeIcon;
        }
    }
}