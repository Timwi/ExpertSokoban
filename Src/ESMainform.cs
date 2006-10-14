using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ExpertSokoban
{
    public partial class ESMainform : Form
    {
        private String filePath;
        private String levelFileName;
        private int levelNumber;
        private SokobanLevel[] currentLevels;
        private bool editing;

        public ESMainform()
        {
            InitializeComponent();
            currentLevels = new SokobanLevel[] { SokobanLevel.getTestLevel() };
            filePath = null;
            levelFileName = null;
            levelNumber = 0;
            editing = false;
            MainArea.SetLevel(currentLevels[0]);
        }

        private void ToolExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ToolUndo_Click(object sender, EventArgs e)
        {
            MainArea.undo();
        }
    }
}