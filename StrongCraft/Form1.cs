using StrongCraft.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing;
using System.Drawing.Imaging;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using System.Runtime.InteropServices;
using System.Collections.ObjectModel;
using System.Diagnostics.Eventing.Reader;
using System.Security.Policy;
using System.Threading;

namespace StrongCraft
{
    public partial class Form1 : Form
    {
        public static int MapSize = 4;
        public int[,] Blocks = new int[64, 64];
        public int shiftx = -43;
        public int shifty = -43;
        public int OldX;
        public int OldY;
        public bool CanBuild;
        public Bitmap MapBitmap = new Bitmap(512 * MapSize, 512 * MapSize);
        public Bitmap nowBitmap;
        private PictureBox pictureBox1 = new PictureBox();
        private PictureBox miniMap = new PictureBox();
        bool IsPressed = false;
        bool Creation = false;
        bool Delete = false;
        bool Moving = false;
        bool Extract = false;
        bool InMap = false;
        public Bitmap nowBuilding;
        public Point nowPoint;
        int CreationIndice = -1;
        int NowGold = 2000;
        int NowFood = 20;
        int NowWood = 100;
        int NowStone = 60;
        public Point SelectedPoint;
        int SelectedSize = 0;
        int progress;
        int TrainingIndice = 0;
        int SpawnIndice = 0;
        Point[] ListOfMovingCharacters = new Point[3];
        List<Point>[] ListOfPathes = new List<Point>[3];
        int[] IndicesOfPathes = new int[3];

        Point[] ListOfMovingEnemies = new Point[4];
        List<Point>[] ListOfEnemyPathes = new List<Point>[4];
        int[] IndicesOfEnemyPathes = new int[4];
        Unit SelectedUnit = new Unit();
        
        Time actualTime = new Time()
        {
            seconds = 0,
            minutes = 0
        };
        public List<Block> AllBuildings = new List<Block>();
        public List<Block> ActiveElements = new List<Block>();
        public List<Unit> Enemies = new List<Unit>();
        public WMPLib.WindowsMediaPlayer WMP = new WMPLib.WindowsMediaPlayer();
        public Form1()
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            miniMap.MouseDown += MiniMap_MouseDown;
            miniMap.MouseMove += MiniMap_MouseMove;
            miniMap.MouseUp += MiniMap_MouseUp;
            pictureBox1.Paint += PictureBox1_Paint;
            pictureBox1.MouseDown += PictureBox1_MouseDown;
            pictureBox1.MouseMove += PictureBox1_MouseMove;
            pictureBox1.MouseUp += PictureBox1_MouseUp;
            pictureBox1.MouseClick += PictureBox1_MouseClick;
            miniMap.Paint += MiniMap_Paint;
            pictureBox1.MouseLeave += PictureBox1_MouseLeave;
            pictureBox1.MouseEnter += PictureBox1_MouseEnter;
            miniMap.SizeMode = PictureBoxSizeMode.Zoom;
            Portrait.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Controls.Add(miniMap);
            this.Controls.Add(pictureBox1);
            pictureBox1.Location = new Point(426, 0);
            miniMap.Location = new Point(22, 0);
            pictureBox3.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox4.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox5.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox6.SizeMode = PictureBoxSizeMode.StretchImage;
            FoodAmount.Text = NowFood.ToString();
            WoodAmount.Text = NowWood.ToString();
            GoldAmount.Text = NowGold.ToString();
            StoneAmount.Text = NowStone.ToString();
            NecessaryResources.Visible = false;
            InGameMessage.Visible = false;

            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            Information.Visible = false;
            ShowTHVisible();
            /*string source = "C:\\Users\\zahar\\OneDrive\\Рабочий стол\\Map\\Sounds\\HumanTheme";
            Random rnd = new Random();
            switch(rnd.Next(1, 4))
            {
                case 1:
                    source += "1";
                    break;
                case 2:
                    source += "2";
                    break;
                case 3:
                    source += "3";
                    break;
            }
            WMP.URL = source + ".wav";
            WMP.controls.stop();*/
        }
        private void InRange()
        {
            for(int i = 0; i < ActiveElements.Count; ++i)
            {
                if (ActiveElements[i].Type == "Unit")
                {
                    Unit unit = ActiveElements[i] as Unit;
                    if(unit.target == null)
                    {
                        bool havetarget = false;
                        Unit LikelyTarget = null;
                        for(int x = unit.iX - unit.range; x <= unit.iX + unit.range; x++)
                        {
                            for(int y = unit.iY - unit.range; y <= unit.iY + unit.range; y++)
                            {
                                for (int j = 0; j < Enemies.Count; ++j)
                                {
                                    if (Enemies[j].iX == x && Enemies[j].iY == y)
                                    {
                                        LikelyTarget = Enemies[j];
                                        havetarget = true;
                                        break;
                                    }
                                }
                                if (havetarget)
                                    break;
                            }
                            if (havetarget)
                                break;
                        }
                        if (havetarget)
                        {
                            unit.target = LikelyTarget;
                            unit.AttackIndice = 0;
                            StoppedUnit(unit);
                        }
                    }else
                    {
                        bool havetarget = false;
                        Unit LikelyTarget = null;
                        for (int x = unit.iX - unit.range; x <= unit.iX + unit.range; x++)
                        {
                            for (int y = unit.iY - unit.range; y <= unit.iY + unit.range; y++)
                            {
                                for (int j = 0; j < Enemies.Count; ++j)
                                {
                                    if (Enemies[j].iX == x && Enemies[j].iY == y)
                                    {
                                        LikelyTarget = Enemies[j];
                                        havetarget = true;
                                        break;
                                    }
                                }
                                if (havetarget)
                                    break;
                            }
                            if (havetarget)
                                break;
                        }
                        if (havetarget)
                        {
                            unit.target = LikelyTarget;
                            unit.AttackIndice = 0;
                            StoppedUnit(unit);
                        }
                        else
                        {
                            unit.target = null;
                        }
                    }
                }
            }
            for(int i = 0; i <  Enemies.Count; ++i)
            {
                if (Enemies[i].target == null && Enemies[i].buildingtarget == null)
                {
                    bool havetarget = false;
                    Unit LikelyTarget = null;
                    for (int x = Enemies[i].iX - Enemies[i].range; x <= Enemies[i].iX + Enemies[i].range; x++)
                    {
                        for (int y = Enemies[i].iY - Enemies[i].range; y <= Enemies[i].iY + Enemies[i].range; y++)
                        {
                            for (int j = 0; j < ActiveElements.Count; ++j)
                            {
                                if (ActiveElements[j].iX == x && ActiveElements[j].iY == y && ActiveElements[j].Type == "Unit")
                                {
                                    LikelyTarget = ActiveElements[j] as Unit;
                                    havetarget = true;
                                    break;
                                }
                            }
                            if (havetarget)
                                break;
                        }
                        if (havetarget)
                            break;
                    }
                    if (havetarget)
                    {
                        Enemies[i].target = LikelyTarget;
                        Enemies[i].AttackIndice = 0;
                    }
                }else if (Enemies[i].buildingtarget != null)
                {
                    StoppedUnit(Enemies[i]);
                }
            }
        }
        private void StoppedUnit(Unit stoppedunit)
        {
            for (int i = 0; i < ActiveElements.Count; ++i)
            {
                if (ActiveElements[i].Type == "Unit" && ActiveElements[i].iX == stoppedunit.iX && ActiveElements[i].iY == stoppedunit.iY)
                {
                    for (int j = 0; j < 3; ++j)
                    {
                        if (ListOfMovingCharacters[j].X == ActiveElements[i].iX && ListOfMovingCharacters[j].Y == ActiveElements[i].iY)
                        {
                            Unit unit1 = ActiveElements[i] as Unit;
                            Point Nextpoint = new Point();
                            Point point = new Point(unit1.iX, unit1.iY);
                            int side = 0;
                            if (j == 0)
                            {
                                if (ListOfPathes[0].Count > 0)
                                {
                                    ListOfMovingCharacters[0] = new Point(-1, -1);
                                    Nextpoint = ListOfPathes[0][0];
                                    side = FindSide(point, Nextpoint);
                                    unit1.Frame = GetBMPofFrame(side, 5, unit1.BlockIndice);
                                }
                                else
                                {
                                    unit1.Frame = GetBMPofFrame(side, 5, unit1.BlockIndice);
                                }
                                Moving_Timer1.Stop();
                            }
                            else if (j == 1)
                            {
                                if (ListOfPathes[1].Count > 0)
                                {
                                    ListOfMovingCharacters[1] = new Point(-1, -1);
                                    Nextpoint = ListOfPathes[1][0];
                                    side = FindSide(point, Nextpoint);
                                    unit1.Frame = GetBMPofFrame(side, 5, unit1.BlockIndice);
                                }
                                else
                                {
                                    side = FindSide(point, point);
                                    unit1.Frame = GetBMPofFrame(side, 5, unit1.BlockIndice);
                                }
                                Moving_Timer2.Stop();
                            }
                            else if (j == 2)
                            {
                                if (ListOfPathes[2].Count > 0)
                                {
                                    Nextpoint = ListOfPathes[2][0];
                                    side = FindSide(point, Nextpoint);
                                    ListOfMovingCharacters[2] = new Point(-1, -1);
                                    unit1.Frame = GetBMPofFrame(side, 5, unit1.BlockIndice);
                                }
                                else
                                {
                                    side = FindSide(point, point);
                                    unit1.Frame = GetBMPofFrame(side, 5, unit1.BlockIndice);
                                }
                                Moving_Timer3.Stop();
                            }
                            unit1.realPoint = new Point(unit1.iX * 32, unit1.iY * 32);
                            ActiveElements.RemoveAt(i);
                            ActiveElements.Add(unit1);
                            pictureBox1.Refresh();
                            miniMap.Refresh();
                        }
                    }
                }
            }
            for (int i = 0; i < Enemies.Count; ++i)
            {
                if (Enemies[i].iX == stoppedunit.iX && Enemies[i].iY == stoppedunit.iY)
                {
                    for (int j = 0; j < 4; ++j)
                    {
                        if (ListOfMovingEnemies[j].X == Enemies[i].iX && ListOfMovingEnemies[j].Y == Enemies[i].iY)
                        {
                            Unit unit1 = Enemies[i];
                            Point Nextpoint = new Point();
                            Point point = new Point(unit1.iX, unit1.iY);
                            int side = 0;
                            if (j == 0)
                            {
                                if (ListOfEnemyPathes[0].Count > 0)
                                {
                                    ListOfMovingEnemies[0] = new Point(-1, -1);
                                    Nextpoint = ListOfEnemyPathes[0][0];
                                    side = FindSide(point, Nextpoint);
                                    unit1.Frame = GetBMPofFrame(side, 5, unit1.BlockIndice);
                                }
                                Enemy_moving1.Stop();
                            }
                            else if (j == 1)
                            {
                                if (ListOfEnemyPathes[1].Count > 0)
                                {
                                    ListOfMovingEnemies[1] = new Point(-1, -1);
                                    Nextpoint = ListOfEnemyPathes[1][0];
                                    side = FindSide(point, Nextpoint);
                                    unit1.Frame = GetBMPofFrame(side, 5, unit1.BlockIndice);
                                }
                                Enemy_moving2.Stop();
                            }
                            else if (j == 2)
                            {
                                if (ListOfEnemyPathes[2].Count > 0)
                                {
                                    Nextpoint = ListOfEnemyPathes[2][0];
                                    side = FindSide(point, Nextpoint);
                                    ListOfMovingEnemies[2] = new Point(-1, -1);
                                    unit1.Frame = GetBMPofFrame(side, 5, unit1.BlockIndice);
                                }
                                Enemy_moving3.Stop();
                            }else if(j == 3)
                            {
                                if (ListOfEnemyPathes[3].Count > 0)
                                {
                                    Nextpoint = ListOfEnemyPathes[3][0];
                                    side = FindSide(point, Nextpoint);
                                    ListOfMovingEnemies[2] = new Point(-1, -1);
                                    unit1.Frame = GetBMPofFrame(side, 5, unit1.BlockIndice);
                                }
                                Enemy_moving3.Stop();
                            }
                            unit1.realPoint = new Point(unit1.iX * 32, unit1.iY * 32);
                            Enemies.RemoveAt(i);
                            Enemies.Add(unit1);
                            pictureBox1.Refresh();
                            miniMap.Refresh();
                        }
                    }
                }
            }
        }
        private int FindSide(Point unit1, Point Nextpoint)
        {
            int side = 5;
            if (unit1.X == Nextpoint.X && unit1.Y != Nextpoint.Y)
            {
                if (unit1.Y > Nextpoint.Y)
                {
                    side = 1;
                }
                else
                {
                    side = 5;
                }
            }
            else if (unit1.Y == Nextpoint.Y && unit1.X != Nextpoint.X)
            {
                if (unit1.X > Nextpoint.X)
                {
                    side = -3;
                }
                else
                {
                    side = 3;
                }
            }
            else if (unit1.X != Nextpoint.X && unit1.Y != Nextpoint.Y)
            {
                if (unit1.X > Nextpoint.X && unit1.Y > Nextpoint.Y)
                {
                    side = -2;
                }
                else if (unit1.X > Nextpoint.X && unit1.Y < Nextpoint.Y)
                {
                    side = -4;
                }
                else if (unit1.X < Nextpoint.X && unit1.Y > Nextpoint.Y)
                {
                    side = 2;
                }
                else if (unit1.X < Nextpoint.X && unit1.Y < Nextpoint.Y)
                {
                    side = 4;
                }
            }
            return side;
        }
        private Bitmap GetBMPofFrame(int side, int moveindice, int blockindice)
        {
            string NameOfFile = "";
            bool mirror = false;
            if (blockindice == 9)
                NameOfFile = "footman";
            else if (blockindice == 10)
                NameOfFile = "archer";
            else if (blockindice == 12)
                NameOfFile = "ogre";
            else if (blockindice == 13)
                NameOfFile = "catapulte";
            if (moveindice == 5 && NameOfFile != "catapulte")
                NameOfFile = NameOfFile + "_stand";
            else
            {
                if(NameOfFile == "catapulte")
                {
                    if(moveindice % 2 == 0)
                    {
                        moveindice = 2;
                    }
                    else
                    {
                        moveindice = 1;
                    }
                }
                NameOfFile = NameOfFile + "_moving" + moveindice.ToString() + "_";
            }
            if(side < 0)
            {
                side = -side;
                mirror = true;
            }
            NameOfFile = NameOfFile + side.ToString();
            Object r = Properties.Resources.ResourceManager.GetObject(NameOfFile);
            Bitmap frame = (Bitmap)r;
            if (mirror)
            {
                frame.RotateFlip(RotateFlipType.RotateNoneFlipX);
            }
            return frame;
        }
        private Bitmap GetBMPforAttack(int side, int attackindice, int blockindice)
        {
            string NameOfFile = "";
            bool mirror = false;
            if (blockindice == 9)
                NameOfFile = "footman";
            else if (blockindice == 10)
                NameOfFile = "archer";
            else if (blockindice == 12)
                NameOfFile = "ogre";
            else if (blockindice == 13)
                NameOfFile = "catapulte";
            if (attackindice == 0)
                NameOfFile += "_stand";
            else
                NameOfFile = NameOfFile + "_attack" + attackindice.ToString() + "_";
            if (side < 0)
            {
                side = -side;
                mirror = true;
            }
            NameOfFile = NameOfFile + side.ToString();
            Object r = Properties.Resources.ResourceManager.GetObject(NameOfFile);
            Bitmap frame = (Bitmap)r;
            if (mirror)
            {
                frame.RotateFlip(RotateFlipType.RotateNoneFlipX);
            }
            return frame;
        }
        private Bitmap GetBMPforCorpse(int blockindice)
        {
            string NameOfFile = "";
            if (blockindice == 9)
                NameOfFile = "footman";
            else if (blockindice == 10)
                NameOfFile = "archer";
            else if (blockindice == 12)
                NameOfFile = "ogre";
            else if (blockindice == 13)
                NameOfFile = "catapulte";
            NameOfFile = NameOfFile + "_corpse";
            Object r = Properties.Resources.ResourceManager.GetObject(NameOfFile);
            Bitmap frame = (Bitmap)r;
            return frame;
        }
        private static int GetHeuristicPathLength(Point from, Point to)
        {
            return Math.Abs(from.X - to.X) + Math.Abs(from.Y - to.Y);
        }
        private static double GetDistanceBetweenNeighbours(Point fn, Point sn)
        {
            if (fn.X == sn.X)
                return 1;
            if (fn.Y == sn.Y)
                return 1;
            return Math.Sqrt(2);
        }
        private static Collection<Cell> GetNeighbours(Cell pathCell, Point end, int [,] field, int BlockIndice)
        {
            var result = new Collection<Cell>();
            Point[] neighbourPoints = new Point[8];
            neighbourPoints[0] = new Point(pathCell.position.X - 1, pathCell.position.Y - 1);
            neighbourPoints[1] = new Point(pathCell.position.X, pathCell.position.Y - 1);
            neighbourPoints[2] = new Point(pathCell.position.X + 1, pathCell.position.Y - 1);
            neighbourPoints[3] = new Point(pathCell.position.X - 1, pathCell.position.Y);
            neighbourPoints[4] = new Point(pathCell.position.X + 1, pathCell.position.Y);
            neighbourPoints[5] = new Point(pathCell.position.X - 1, pathCell.position.Y + 1);
            neighbourPoints[6] = new Point(pathCell.position.X, pathCell.position.Y + 1);
            neighbourPoints[7] = new Point(pathCell.position.X + 1, pathCell.position.Y + 1);

            foreach(var point in neighbourPoints)
            {
                if (point.X < 0 || point.X >= field.GetLength(0))
                    continue;
                if (point.Y < 0 || point.Y >= field.GetLength(1))
                    continue;
                /*if (field[point.X, point.Y] != 0)
                    continue;*/
                if(BlockIndice == 9)
                {
                    if (field[point.X, point.Y] != 0)
                        continue;
                }
                else if(BlockIndice == 10 || BlockIndice == 11)
                {
                    if (field[point.X, point.Y] != 0 && field[point.X, point.Y] != 5)
                        continue;
                }else if(BlockIndice == 12 || BlockIndice == 13)
                {
                    if (field[point.X, point.Y] != 0 && field[point.X, point.Y] != 5 && field[point.X, point.Y] != 2 && field[point.X, point.Y] != 6)
                        continue;
                }
                var neighbourCell = new Cell()
                {
                    position = point,
                    parent = pathCell,
                    PathLengthFromStart = pathCell.PathLengthFromStart + GetDistanceBetweenNeighbours(point, pathCell.position),
                    HeuristicEstimatePathLength = GetHeuristicPathLength(point, end)
                };
                result.Add(neighbourCell);
            }
            return result;
        }
        private static List<Point> GetPathForCell(Cell pathCell)
        {
            var result = new List<Point>();
            var currentCell = pathCell;
            while (currentCell != null)
            {
                result.Add(currentCell.position);
                currentCell = currentCell.parent;
            }
            result.Reverse();
            return result;
        }
        private List<Point> FindPath(int [,] field, Point begin, Point end, int BlockIndice)
        {
            List<Cell> ClosedSet = new List<Cell>();
            List<Cell> OpenSet = new List<Cell>();
            Cell startCell = new Cell()
            {
                position = begin,
                parent = null,
                PathLengthFromStart = 0,
                HeuristicEstimatePathLength = GetHeuristicPathLength(begin, end)
            };
            OpenSet.Add(startCell);
            while(OpenSet.Count > 0)
            {
                var currentCell = OpenSet.OrderBy(node => node.EstimateFullPathLength).First();
                if (currentCell.position == end)
                    return GetPathForCell(currentCell);
                OpenSet.Remove(currentCell);
                ClosedSet.Add(currentCell);
                foreach(var neighbourCell in GetNeighbours(currentCell, end, field, BlockIndice))
                {
                    if (ClosedSet.Count(node => node.position == neighbourCell.position) > 0)
                        continue;
                    var openCell = OpenSet.FirstOrDefault(node => node.position == neighbourCell.position);
                    if (openCell == null)
                        OpenSet.Add(neighbourCell);
                    else
                    {
                        if(openCell.PathLengthFromStart > neighbourCell.PathLengthFromStart)
                        {
                            openCell.parent = currentCell;
                            openCell.PathLengthFromStart = neighbourCell.PathLengthFromStart;
                        }
                    }
                }
            }
            int minh = 1000;
            Cell NearestToEnd = null;
            for(int i = 0; i < ClosedSet.Count; ++i)
            {
                if (ClosedSet[i].HeuristicEstimatePathLength < minh)
                {
                    minh = ClosedSet[i].HeuristicEstimatePathLength;
                    NearestToEnd = ClosedSet[i];
                }
            }
            if(NearestToEnd.position != begin)
                return GetPathForCell(NearestToEnd);
            return null;
        }
        private void ShowInfo(int i)
        {
            Object rm = Properties.Resources.ResourceManager.GetObject("BarracksIcon");
            Portrait.Image = (Bitmap)rm;
            NameOfBlock.Text = "Barracks";
            ShowBarracksVisible();
            LevelOfBlock.Text = AllBuildings[i].Lvl.ToString();
            HpOfBlock.Text = AllBuildings[i].Hp.ToString() + " / " + AllBuildings[i].MaxHp;
            Bitmap bmp = new Bitmap(progressHP.Width, progressHP.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);
                float hpwidth = (float)AllBuildings[i].Hp / AllBuildings[i].MaxHp;
                g.FillRectangle(Brushes.ForestGreen, new Rectangle(0, 0, (int)(hpwidth * progressHP.Width), progressHP.Height));
            }
            progressHP.Image = bmp;
            progressHP.Refresh();
            if (AllBuildings[i].Type == "Building")
            {
                Building nowbuilding = AllBuildings[i] as Building;
                if (nowbuilding.Resource == "")
                {
                    InfoLabel.Text = "";
                    InfoValue.Text = "";
                }
                else
                {
                    if (nowbuilding.Resource == "Training")
                    {
                        if (TrainingIndice == 1)
                        {
                            Object r = Properties.Resources.ResourceManager.GetObject("FootManIcon");
                            Training.Image = (Bitmap)r;
                        }
                        else
                        {
                            Object r = Properties.Resources.ResourceManager.GetObject("ArcgerIcon");
                            Training.Image = (Bitmap)r;
                        }
                        Training.Visible = true;
                        InfoLabel.Text = nowbuilding.Resource;
                        InfoValue.Text = nowbuilding.ResourceValue.ToString() + "%";
                    }
                    else
                    {
                        InfoLabel.Text = "Extracted " + nowbuilding.Resource;
                        InfoValue.Text = nowbuilding.ResourceValue.ToString();
                    }
                }
            }
            Information.Visible = true;
        }
        private void HideAllIcons()
        {
            CreateTownHall.Visible = false;
            UpTownHall.Visible = false;
            CreateBarracks.Visible = false;
            CreateFarm.Visible = false;
            CreateWalls.Visible = false;
            DeleteWalls.Visible = false;
            CreateFootMan.Visible = false;
            CreateArcher.Visible = false;
            MoveUnit.Visible = false;
            StopUnit.Visible = false;
            Extraction.Visible = false;
            CancelAction.Visible = false;
            bool haveth = false;
            for (int i = 0; i < ActiveElements.Count; ++i)
            {
                if (ActiveElements[i].BlockIndice == 6)
                {
                    haveth = true;
                }
            }
            if (!haveth)
            {
                CreateTownHall.Visible = true;
            }
        }
        private void ShowTHVisible()
        {
            CreateTownHall.Visible = true;
            UpTownHall.Visible = false;
            CreateBarracks.Visible = false;
            CreateFarm.Visible = false;
            CreateWalls.Visible = false;
            DeleteWalls.Visible = false;
            for (int i = 0; i < ActiveElements.Count; ++i)
            {
                if (ActiveElements[i].BlockIndice == 6)
                {
                    CreateTownHall.Visible = false;
                    UpTownHall.Visible = true;
                    CreateBarracks.Visible = true;
                    CreateFarm.Visible = true;
                    CreateWalls.Visible = true;
                    DeleteWalls.Visible = true;
                    if (ActiveElements[i].Lvl == 2)
                    {
                        UpTownHall.Visible = false;
                    }
                }
                if (ActiveElements[i].BlockIndice == 8)
                {
                    CreateBarracks.Visible = false;
                }
            }
            CreateFootMan.Visible = false;
            CreateArcher.Visible = false;
            MoveUnit.Visible = false;
            StopUnit.Visible = false;
            Extraction.Visible = false;
        }

        private void ShowBarracksVisible()
        {
            CreateTownHall.Visible = false;
            UpTownHall.Visible = false;
            CreateBarracks.Visible = false;
            CreateFarm.Visible = false;
            CreateWalls.Visible = false;
            DeleteWalls.Visible = false;
            CreateFootMan.Visible = true;
            CreateArcher.Visible = false;
            for(int i = 0; i < ActiveElements.Count; ++i)
            {
                if (ActiveElements[i].BlockIndice == 6)
                {
                    if (ActiveElements[i].Lvl == 2)
                    {
                        CreateArcher.Visible = true;
                    }
                }
            }
            MoveUnit.Visible = false;
            StopUnit.Visible = false;
            Extraction.Visible = false;
        }

        private void ShowUnitAction()
        {
            CreateTownHall.Visible = false;
            UpTownHall.Visible = false;
            CreateBarracks.Visible = false;
            CreateFarm.Visible = false;
            CreateWalls.Visible = false;
            DeleteWalls.Visible = false;
            CreateFootMan.Visible = false;
            CreateArcher.Visible = false;
            MoveUnit.Visible = true;
            StopUnit.Visible = true;
            Extraction.Visible = true;
            Training.Visible = false;
        }
        private void PictureBox1_MouseEnter(object sender, EventArgs e)
        {
            InMap = true;
            pictureBox1.Refresh();
        }

        private void PictureBox1_MouseLeave(object sender, EventArgs e)
        {
            InMap = false;
            pictureBox1.Refresh();
        }

        private void UpdateTexture(int iX, int iY)
        {
            if (iX > 1 && iY > 1 && iX < 63 && iY < 63)
            {
                if ((Blocks[iX - 1, iY] == 5 || Blocks[iX - 1, iY] == 11) && (Blocks[iX + 1, iY] == 5 || Blocks[iX + 1, iY] == 11) && (Blocks[iX, iY - 1] == 5 || Blocks[iX, iY - 1] == 11) && (Blocks[iX, iY + 1] == 5 || Blocks[iX, iY + 1] == 11))
                {
                    Object r = Properties.Resources.ResourceManager.GetObject("_34");
                    nowBuilding = (Bitmap)r;
                }

                else if ((Blocks[iX - 1, iY] != 5 && Blocks[iX - 1, iY] != 11) && (Blocks[iX + 1, iY] == 5 || Blocks[iX + 1, iY] == 11) && (Blocks[iX, iY - 1] == 5 || Blocks[iX, iY - 1] == 11) && (Blocks[iX, iY + 1] == 5 || Blocks[iX, iY + 1] == 11))
                {
                    Object r = Properties.Resources.ResourceManager.GetObject("_25");
                    nowBuilding = (Bitmap)r;
                }
                else if ((Blocks[iX - 1, iY] == 5 || Blocks[iX - 1, iY] == 11) && (Blocks[iX + 1, iY] != 5 && Blocks[iX + 1, iY] != 11) && (Blocks[iX, iY - 1] == 5 || Blocks[iX, iY - 1] == 11) && (Blocks[iX, iY + 1] == 5 || Blocks[iX, iY + 1] == 11))
                {
                    Object r = Properties.Resources.ResourceManager.GetObject("_32");
                    nowBuilding = (Bitmap)r;
                }
                else if ((Blocks[iX - 1, iY] == 5 || Blocks[iX - 1, iY] == 11) && (Blocks[iX + 1, iY] == 5 || Blocks[iX + 1, iY] == 11) && (Blocks[iX, iY - 1] != 5 && Blocks[iX, iY - 1] != 11) && (Blocks[iX, iY + 1] == 5 || Blocks[iX, iY + 1] == 11))
                {
                    Object r = Properties.Resources.ResourceManager.GetObject("_30");
                    nowBuilding = (Bitmap)r;
                }
                else if ((Blocks[iX - 1, iY] == 5 || Blocks[iX - 1, iY] == 11) && (Blocks[iX + 1, iY] == 5 || Blocks[iX + 1, iY] == 11) && (Blocks[iX, iY - 1] == 5 || Blocks[iX, iY - 1] == 11) && (Blocks[iX, iY + 1] != 5 && Blocks[iX, iY + 1] != 11))
                {
                    Object r = Properties.Resources.ResourceManager.GetObject("_33");
                    nowBuilding = (Bitmap)r;
                }


                else if ((Blocks[iX - 1, iY] != 5 && Blocks[iX - 1, iY] != 11) && (Blocks[iX + 1, iY] != 5 && Blocks[iX + 1, iY] != 11) && (Blocks[iX, iY - 1] == 5 || Blocks[iX, iY - 1] == 11) && (Blocks[iX, iY + 1] == 5 || Blocks[iX, iY + 1] == 11))
                {
                    Object r = Properties.Resources.ResourceManager.GetObject("_22");
                    nowBuilding = (Bitmap)r;
                }
                else if ((Blocks[iX - 1, iY] != 5 && Blocks[iX - 1, iY] != 11) && (Blocks[iX + 1, iY] == 5 || Blocks[iX + 1, iY] == 11) && (Blocks[iX, iY - 1] != 5 && Blocks[iX, iY - 1] != 11) && (Blocks[iX, iY + 1] == 5 || Blocks[iX, iY + 1] == 11))
                {
                    Object r = Properties.Resources.ResourceManager.GetObject("_20");
                    nowBuilding = (Bitmap)r;
                }
                else if ((Blocks[iX - 1, iY] != 5 && Blocks[iX - 1, iY] != 11) && (Blocks[iX + 1, iY] == 5 || Blocks[iX + 1, iY] == 11) && (Blocks[iX, iY - 1] == 5 || Blocks[iX, iY - 1] == 11) && (Blocks[iX, iY + 1] != 5 && Blocks[iX, iY + 1] != 11))
                {
                    Object r = Properties.Resources.ResourceManager.GetObject("_24");
                    nowBuilding = (Bitmap)r;
                }
                else if ((Blocks[iX - 1, iY] == 5 || Blocks[iX - 1, iY] == 11) && (Blocks[iX + 1, iY] != 5 && Blocks[iX + 1, iY] != 11) && (Blocks[iX, iY - 1] != 5 && Blocks[iX, iY - 1] != 11) && (Blocks[iX, iY + 1] == 5 || Blocks[iX, iY + 1] == 11))
                {
                    Object r = Properties.Resources.ResourceManager.GetObject("_27");
                    nowBuilding = (Bitmap)r;
                }
                else if ((Blocks[iX - 1, iY] == 5 || Blocks[iX - 1, iY] == 11) && (Blocks[iX + 1, iY] != 5 && Blocks[iX + 1, iY] != 11) && (Blocks[iX, iY - 1] == 5 || Blocks[iX, iY - 1] == 11) && (Blocks[iX, iY + 1] != 5 && Blocks[iX, iY + 1] != 11))
                {
                    Object r = Properties.Resources.ResourceManager.GetObject("_31");
                    nowBuilding = (Bitmap)r;
                }
                else if ((Blocks[iX - 1, iY] == 5 || Blocks[iX - 1, iY] == 11) && (Blocks[iX + 1, iY] == 5 || Blocks[iX + 1, iY] == 11) && (Blocks[iX, iY - 1] != 5 && Blocks[iX, iY - 1] != 11) && (Blocks[iX, iY + 1] != 5 && Blocks[iX, iY + 1] != 11))
                {
                    Object r = Properties.Resources.ResourceManager.GetObject("_28");
                    nowBuilding = (Bitmap)r;
                }

                else if ((Blocks[iX - 1, iY] == 5) || (Blocks[iX - 1, iY] == 11))
                {
                    Object r = Properties.Resources.ResourceManager.GetObject("_26");
                    nowBuilding = (Bitmap)r;
                }
                else if ((Blocks[iX + 1, iY] == 5) || (Blocks[iX + 1, iY] == 11))
                {
                    Object r = Properties.Resources.ResourceManager.GetObject("_19");
                    nowBuilding = (Bitmap)r;
                }
                else if ((Blocks[iX, iY - 1] == 5) || (Blocks[iX, iY - 1] == 11))
                {
                    Object r = Properties.Resources.ResourceManager.GetObject("_21");
                    nowBuilding = (Bitmap)r;
                }
                else if ((Blocks[iX, iY + 1] == 5) || (Blocks[iX, iY + 1] == 11))
                {
                    Object r = Properties.Resources.ResourceManager.GetObject("_18");
                    nowBuilding = (Bitmap)r;
                }
                else
                {
                    Object r = Properties.Resources.ResourceManager.GetObject("_17");
                    nowBuilding = (Bitmap)r;
                }
                if (Delete && Blocks[iX, iY] == -1)
                {
                    Object r = Properties.Resources.ResourceManager.GetObject("_01");
                    nowBuilding = (Bitmap)r;
                    Blocks[iX, iY] = 0;
                }else if(Delete && Blocks[iX, iY] == 10)
                {
                    Object r = Properties.Resources.ResourceManager.GetObject("_01");
                    nowBuilding = (Bitmap)r;
                }
            }
        }
        private void PictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if ((Creation) && (e.Button == MouseButtons.Left))
            {
                using (Graphics g = Graphics.FromImage(MapBitmap))
                {
                    int iX = ((int)MapBitmap.Width / 2 - (int)MapBitmap.Width / 6 + shiftx + nowPoint.X) / 32;
                    int iY = ((int)MapBitmap.Width / 2 - (int)MapBitmap.Width / 6 + shifty + nowPoint.Y) / 32;
                    if (iX <= 63 && iY <= 63 && iX >= 0 && iY >= 0)
                    {
                        if (Blocks[iX, iY] == 0)
                        {
                            if(CreationIndice == 1)
                            {
                                if (NowWood >= 3 && NowStone >= 1)
                                {
                                    Blocks[iX, iY] = 5;
                                    for (int x = iX - 1; x <= iX + 1; ++x)
                                    {
                                        for (int y = iY - 1; y <= iY + 1; ++y)
                                        {
                                            if (Blocks[x, y] == 5)
                                            {
                                                if (x < 64 && y < 64)
                                                {
                                                    UpdateTexture(x, y);
                                                    g.DrawImage(nowBuilding, x * 32, y * 32);
                                                }
                                            }
                                        }
                                    }
                                    Building Wall = new Building(iX, iY, 5, 1, 1, 40, 40, "Building", "", 0, false);
                                    AllBuildings.Add(Wall);
                                    ActiveElements.Add(Wall);
                                    NowStone -= 1;
                                    StoneAmount.Text = NowStone.ToString();
                                    NowWood -= 3;
                                    WoodAmount.Text = NowWood.ToString();
                                }
                                else
                                {
                                    InGameMessage.Visible = true;
                                    InGameMessage.Text = "You don't have enough resources";
                                    MessageTimer.Stop();
                                    MessageTimer.Start();
                                    NecessaryResources.Visible = false;
                                    Creation = false;
                                    CancelAction.Visible = false;
                                    SelectedSize = 0;
                                }
                                
                            }
                            else if (CreationIndice == 2)
                            {
                                bool busy = false;
                                for (int i = iX; i < iX + CreationIndice; i++)
                                {
                                    for (int j = iY; j < iY + CreationIndice; j++)
                                    {
                                        if (Blocks[i, j] != 0)
                                        {
                                            busy = true;
                                            break;
                                        }
                                    }
                                    if (busy == true)
                                        break;
                                }
                                if (!busy)
                                {
                                    g.DrawImage(nowBuilding, new Rectangle(iX * 32, iY * 32, 32 * CreationIndice, 32 * CreationIndice));
                                    for (int i = iX; i < iX + CreationIndice; i++)
                                    {
                                        for (int j = iY; j < iY + CreationIndice; j++)
                                        {
                                            Blocks[i, j] = 2;
                                        }
                                    }
                                    Building FRM = new Building(iX, iY, 2, 2, 1, 20, 20, "Building", "Food", 1, false);
                                    ActiveElements.Add(FRM);
                                    AllBuildings.Add(FRM);
                                    Creation = false;
                                    SelectedSize = 0;
                                    NowWood -= 40;
                                    WoodAmount.Text = NowWood.ToString();
                                    NowStone -= 3;
                                    StoneAmount.Text = NowStone.ToString();
                                    CancelAction.Visible = false;
                                }
                            }
                            else if (CreationIndice == 3)
                            {
                                bool busy = false;
                                for (int i = iX; i < iX + CreationIndice; i++)
                                {
                                    for (int j = iY; j < iY + CreationIndice; j++)
                                    {
                                        if (Blocks[i, j] != 0)
                                        {
                                            busy = true;
                                            break;
                                        }
                                    }
                                    if (busy == true)
                                        break;
                                }
                                if (!busy)
                                {
                                    g.DrawImage(nowBuilding, new Rectangle(iX * 32, iY * 32, 32 * CreationIndice, 32 * CreationIndice));
                                    for (int i = iX; i < iX + CreationIndice; i++)
                                    {
                                        for (int j = iY; j < iY + CreationIndice; j++)
                                        {
                                            Blocks[i, j] = 8;
                                        }
                                    }
                                    Building BRCKS = new Building(iX, iY, 8, 3, 1, 60, 60, "Building", "", 0, false);
                                    ActiveElements.Add(BRCKS);
                                    AllBuildings.Add(BRCKS);
                                    Creation = false;
                                    SelectedSize = 0;
                                    NowGold -= 200;
                                    GoldAmount.Text = NowGold.ToString();
                                    NowStone -= 5;
                                    StoneAmount.Text = NowStone.ToString();
                                    CancelAction.Visible = false;
                                }
                            }
                            else if(CreationIndice == 4)
                            {
                                bool busy = false;
                                for (int i = iX; i < iX + CreationIndice; i++)
                                {
                                    for (int j = iY; j < iY + CreationIndice; j++)
                                    {
                                        if (Blocks[i, j] != 0)
                                        {
                                            busy = true;
                                            break;
                                        }
                                    }
                                    if (busy == true)
                                        break;
                                }
                                if (!busy)
                                {
                                    g.DrawImage(nowBuilding, new Rectangle(iX * 32, iY * 32, 32 * CreationIndice, 32 * CreationIndice));
                                    for (int i = iX; i < iX + CreationIndice; i++)
                                    {
                                        for (int j = iY; j < iY + CreationIndice; j++)
                                        {
                                            Blocks[i, j] = 6;
                                        }
                                    }
                                    Building TH = new Building(iX, iY, 6, 4, 1, 80, 80, "Building", "", 0, false);
                                    ActiveElements.Add(TH);
                                    AllBuildings.Add(TH);
                                    Creation = false;
                                    SelectedSize = 0;
                                    NowGold -= 400;
                                    GoldAmount.Text = NowGold.ToString();
                                    NowStone -= 16;
                                    StoneAmount.Text = NowStone.ToString();
                                    CancelAction.Visible = false;
                                    HideAllIcons();
                                    if (actualTime.minutes < 10 || actualTime.seconds < 10)
                                    {
                                        if (actualTime.minutes < 10 && actualTime.seconds < 10)
                                        {
                                            ActualTime.Text = "0" + actualTime.minutes.ToString() + " : 0" + actualTime.seconds.ToString();
                                        }
                                        else if (actualTime.minutes < 10)
                                        {
                                            ActualTime.Text = "0" + actualTime.minutes.ToString() + " : " + actualTime.seconds.ToString();
                                        }
                                        else
                                        {
                                            ActualTime.Text = actualTime.minutes.ToString() + " : 0" + actualTime.seconds.ToString();
                                        }
                                    }
                                    GlobalTimer.Start();
                                    Attack_timer.Start();
                                    Spawn_Enemies.Start();
                                    WMP.controls.play();
                                    WMP.settings.volume = 50;
                                }
                            }
                        }
                    }
                }
            }
            else if(Creation && e.Button == MouseButtons.Right)
            {
                if (Creation || Delete || Moving || Extract)
                {
                    if (Creation)
                        Creation = false;
                    if (Delete)
                        Delete = false;
                    if(Moving) 
                        Moving = false;
                    if(Extract)
                        Extract = false;
                }
                CancelAction.Visible = false;
                bool haveth = false;
                for (int i = 0; i < 64; ++i)
                {
                    for (int j = 0; j < 64; ++j)
                    {
                        if (Blocks[i, j] == 6)
                        {
                            haveth = true;
                        }
                    }
                }
                if (!haveth)
                {
                    ShowTHVisible();
                }
                pictureBox1.Refresh();
                SelectedSize = 0;
            }
            else if ((Delete) && (e.Button == MouseButtons.Left))
            {
                using (Graphics g = Graphics.FromImage(MapBitmap))
                {
                    int iX = ((int)MapBitmap.Width / 2 - (int)MapBitmap.Width / 6 + shiftx + nowPoint.X) / 32;
                    int iY = ((int)MapBitmap.Width / 2 - (int)MapBitmap.Width / 6 + shifty + nowPoint.Y) / 32;
                    if (iX <= 63 && iY <= 63 && iX >= 0 && iY >= 0)
                    {
                        if (Blocks[iX, iY] == 5)
                        {
                            Blocks[iX, iY] = -1;
                            for (int x = iX - 1; x <= iX + 1; ++x)
                            {
                                for (int y = iY - 1; y <= iY + 1; ++y)
                                {
                                    if (Blocks[x, y] == 5 || Blocks[x, y] == -1 || Blocks[x, y] == 11)
                                    {
                                        UpdateTexture(x, y);
                                        g.DrawImage(nowBuilding, x * 32, y * 32);
                                    }
                                }
                            }
                            NowStone += 1;
                            StoneAmount.Text = NowStone.ToString();
                            NowWood += 3;
                            WoodAmount.Text = NowWood.ToString();
                        }
                        else if (Blocks[iX, iY] == 11)
                        {
                            Blocks[iX, iY] = 10;
                            for (int x = iX - 1; x <= iX + 1; ++x)
                            {
                                for (int y = iY - 1; y <= iY + 1; ++y)
                                {
                                    if (Blocks[x, y] == 5 || Blocks[x, y] == 10 || Blocks[x, y] == 10)
                                    {
                                        UpdateTexture(x, y);
                                        g.DrawImage(nowBuilding, x * 32, y * 32);
                                    }
                                }
                            }
                            NowStone += 1;
                            StoneAmount.Text = NowStone.ToString();
                            NowWood += 3;
                            WoodAmount.Text = NowWood.ToString();
                        }
                        for(int i = 0; i < ActiveElements.Count; ++i)
                        {
                            if (ActiveElements[i].iX == iX && ActiveElements[i].iY == iY && ActiveElements[i].Type == "Building")
                            {
                                ActiveElements.RemoveAt(i);
                            } 
                        }
                        for (int i = 0; i < AllBuildings.Count; ++i)
                        {
                            if (AllBuildings[i].iX == iX && AllBuildings[i].iY == iY)
                            {
                                AllBuildings.RemoveAt(i);
                            }
                        }
                    }
                }
            }
            else if((Moving) && (e.Button == MouseButtons.Left))
            {
                int iX = ((int)MapBitmap.Width / 2 - (int)MapBitmap.Width / 6 + shiftx + nowPoint.X) / 32;
                int iY = ((int)MapBitmap.Width / 2 - (int)MapBitmap.Width / 6 + shifty + nowPoint.Y) / 32;
                int BlockIndice = -2;
                Unit unit = new Unit();
                for(int i = 0; i < ActiveElements.Count; ++i)
                {
                    if (ActiveElements[i].iX == SelectedPoint.X && ActiveElements[i].iY == SelectedPoint.Y && ActiveElements[i].Type == "Unit")
                    {
                        BlockIndice = ActiveElements[i].BlockIndice;
                        unit = ActiveElements[i] as Unit;
                        break;
                    }
                }
                if(BlockIndice == 9 || BlockIndice == 10 || BlockIndice == 11 || unit.target == null)
                {
                    List<Point> Path = FindPath(Blocks, SelectedPoint, new Point(iX, iY), BlockIndice);
                    if (Path != null)
                    {
                        Path.RemoveAt(0);
                        if (!Moving_Timer1.Enabled)
                        {
                            ListOfPathes[0] = Path;
                            ListOfMovingCharacters[0] = SelectedPoint;
                            IndicesOfPathes[0] = 0;
                            Moving_Timer1.Start();
                        }
                        else if (!Moving_Timer2.Enabled)
                        {
                            ListOfPathes[1] = Path;
                            ListOfMovingCharacters[1] = SelectedPoint;
                            IndicesOfPathes[1] = 0;
                            Moving_Timer2.Start();
                        }
                        else if (!Moving_Timer3.Enabled)
                        {
                            ListOfPathes[2] = Path;
                            ListOfMovingCharacters[2] = SelectedPoint;
                            IndicesOfPathes[2] = 0;
                            Moving_Timer3.Start();
                        }
                        else
                        {
                            InGameMessage.Visible = true;
                            InGameMessage.Text = "The maximum number of timers has already been reached (3)";
                            MessageTimer.Stop();
                            MessageTimer.Start();
                        }
                        Moving = false;
                        SelectedSize = 0;
                        CancelAction.Visible = false;
                    }
                }
                else
                {
                    InGameMessage.Visible = true;
                    if (unit.target != null)
                    {
                        InGameMessage.Text = "The unit is fighting";
                    }else
                        InGameMessage.Text = "No unit was selected";
                    MessageTimer.Stop();
                    MessageTimer.Start();
                    Moving = false;
                    SelectedSize = 0;
                    CancelAction.Visible = false;
                }
            }
            else if((Extract) && (e.Button == MouseButtons.Left))
            {
                int iX = ((int)MapBitmap.Width / 2 - (int)MapBitmap.Width / 6 + shiftx + nowPoint.X) / 32;
                int iY = ((int)MapBitmap.Width / 2 - (int)MapBitmap.Width / 6 + shifty + nowPoint.Y) / 32;
                if (Math.Abs(SelectedPoint.X - iX) <= 1 && Math.Abs(SelectedPoint.Y - iY) <= 1 && !(SelectedPoint.X == iX && SelectedPoint.Y == iY))
                {
                    int BlockIndice = -2;
                    for (int i = 0; i < ActiveElements.Count; ++i)
                    {
                        if (ActiveElements[i].iX == SelectedPoint.X && ActiveElements[i].iY == SelectedPoint.Y && ActiveElements[i].Type == "Unit")
                        {
                            BlockIndice = ActiveElements[i].BlockIndice;
                            break;
                        }
                    }
                    if(BlockIndice == 9 || BlockIndice == 10 || BlockIndice == 11)
                    {
                        if (Blocks[iX, iY] == 2 || Blocks[iX, iY] == 3 || Blocks[iX, iY] == 4)
                        {
                            for(int i = 0; i < AllBuildings.Count; ++i)
                            {
                                if (AllBuildings[i].Type == "Building" && iX >= AllBuildings[i].iX && iX <= AllBuildings[i].iX + AllBuildings[i].Size - 1 && iY >= AllBuildings[i].iY && iY <= AllBuildings[i].iY + AllBuildings[i].Size - 1)
                                {
                                    Building building = AllBuildings[i] as Building;
                                    if(building.ResourceValue == 0)
                                    {
                                        building.ResourceValue = 1;
                                        Extract = false;
                                        SelectedSize = 0;
                                        CancelAction.Visible = false;
                                        using (Graphics g = Graphics.FromImage(MapBitmap))
                                        {
                                            for (int x = building.iX; x < building.iX + building.Size; x++)
                                            {
                                                for (int y = building.iY; y < building.iY + building.Size; y++)
                                                {
                                                    Object r = Properties.Resources.ResourceManager.GetObject("_01");
                                                    Bitmap grass = (Bitmap)r;
                                                    g.DrawImage(grass, x * 32, y * 32);
                                                }
                                            }
                                            if (building.Resource == "Gold")
                                            {
                                                Object r = Properties.Resources.ResourceManager.GetObject("_1_8");
                                                Bitmap goldmine = (Bitmap)r;
                                                g.DrawImage(goldmine, building.iX * 32, building.iY * 32);
                                            }
                                            if (building.Resource == "Food")
                                            {
                                                Object r = Properties.Resources.ResourceManager.GetObject("_1_4_1");
                                                Bitmap farm = (Bitmap)r;
                                                g.DrawImage(farm, building.iX * 32, building.iY * 32);
                                            }
                                            if (building.Resource == "Stone")
                                            {
                                                Object r = Properties.Resources.ResourceManager.GetObject("_1_9");
                                                Bitmap stone = (Bitmap)r;
                                                g.DrawImage(stone, building.iX * 32, building.iY * 32);
                                            }

                                        }
                                        pictureBox1.Refresh();
                                        miniMap.Refresh();
                                        InGameMessage.Visible = true;
                                        InGameMessage.Text = "Now the building brings resources";
                                        MessageTimer.Stop();
                                        MessageTimer.Start();
                                    }
                                    else
                                    {
                                        InGameMessage.Visible = true;
                                        InGameMessage.Text = "The building has already been taken over";
                                        MessageTimer.Stop();
                                        MessageTimer.Start();
                                    }
                                }
                            }

                        }
                        else
                        {
                            InGameMessage.Visible = true;
                            InGameMessage.Text = "There's nothing to take here";
                            MessageTimer.Stop();
                            MessageTimer.Start();
                        }
                    }
                }
            }
            else if(!Creation && !Delete && !Moving && !Extract && e.Button == MouseButtons.Left)
            {
                int iX = ((int)MapBitmap.Width / 2 - (int)MapBitmap.Width / 6 + shiftx + nowPoint.X) / 32;
                int iY = ((int)MapBitmap.Width / 2 - (int)MapBitmap.Width / 6 + shifty + nowPoint.Y) / 32;
                if (iX <= 63 && iY <= 63 && iX >= 0 && iY >= 0)
                {
                    for (int i = 0; i < AllBuildings.Count; ++i)
                    {
                        if (iX >= AllBuildings[i].iX && iX < AllBuildings[i].iX + AllBuildings[i].Size && iY >= AllBuildings[i].iY && iY < AllBuildings[i].iY + AllBuildings[i].Size)
                        {
                            Training.Visible = false;
                            SelectedPoint = new Point(AllBuildings[i].iX, AllBuildings[i].iY);
                            SelectedSize = AllBuildings[i].Size;
                            if (AllBuildings[i].BlockIndice == 2)
                            {
                                Object r = Properties.Resources.ResourceManager.GetObject("FarmIcon");
                                Portrait.Image = (Bitmap)r;
                                NameOfBlock.Text = "Farm";
                                HideAllIcons();
                            }
                            else if (AllBuildings[i].BlockIndice == 3)
                            {
                                Object r = Properties.Resources.ResourceManager.GetObject("MineIcon");
                                Portrait.Image = (Bitmap)r;
                                NameOfBlock.Text = "Gold Mine";
                                HideAllIcons();
                            }
                            else if (AllBuildings[i].BlockIndice == 4)
                            {
                                Object r = Properties.Resources.ResourceManager.GetObject("StoneIcon");
                                Portrait.Image = (Bitmap)r;
                                NameOfBlock.Text = "Bunch of Rocks";
                                HideAllIcons();
                            }
                            else if (AllBuildings[i].BlockIndice == 5)
                            {
                                Object r = Properties.Resources.ResourceManager.GetObject("default_wall");
                                if (AllBuildings[i].Lvl == 2)
                                {
                                    r = Properties.Resources.ResourceManager.GetObject("Lvl2WallIcon");
                                }
                                Portrait.Image = (Bitmap)r;
                                NameOfBlock.Text = "Wall";
                                HideAllIcons();
                            }
                            else if (AllBuildings[i].BlockIndice == 6)
                            {
                                Object r = Properties.Resources.ResourceManager.GetObject("THIcon");
                                if (AllBuildings[i].Lvl == 2)
                                {
                                    r = Properties.Resources.ResourceManager.GetObject("UpTHLvL2");
                                }
                                Portrait.Image = (Bitmap)r;
                                NameOfBlock.Text = "Town Hall";
                                ShowTHVisible();
                            }
                            else if (AllBuildings[i].BlockIndice == 8)
                            {
                                Object r = Properties.Resources.ResourceManager.GetObject("BarracksIcon");
                                Portrait.Image = (Bitmap)r;
                                NameOfBlock.Text = "Barracks";
                                ShowBarracksVisible();
                            }
                            LevelOfBlock.Text = AllBuildings[i].Lvl.ToString();
                            HpOfBlock.Text = AllBuildings[i].Hp.ToString() + " / " + AllBuildings[i].MaxHp;
                            Bitmap bmp = new Bitmap(progressHP.Width, progressHP.Height);
                            using (Graphics g = Graphics.FromImage(bmp))
                            {
                                g.Clear(Color.White);
                                float hpwidth = (float)AllBuildings[i].Hp / AllBuildings[i].MaxHp;
                                g.FillRectangle(Brushes.ForestGreen, new Rectangle(0, 0, (int)(hpwidth * progressHP.Width), progressHP.Height));
                            }

                            progressHP.Image = bmp;
                            progressHP.Refresh();
                            if (AllBuildings[i].Type == "Building")
                            {
                                Building nowbuilding = AllBuildings[i] as Building;
                                if (nowbuilding.Resource == "")
                                {
                                    InfoLabel.Text = "";
                                    InfoValue.Text = "";
                                }
                                else
                                {
                                    if (nowbuilding.Resource == "Training")
                                    {
                                        if (TrainingIndice == 1)
                                        {
                                            Object r = Properties.Resources.ResourceManager.GetObject("FootManIcon");
                                            Training.Image = (Bitmap)r;
                                        }
                                        else
                                        {
                                            Object r = Properties.Resources.ResourceManager.GetObject("ArcgerIcon");
                                            Training.Image = (Bitmap)r;
                                        }
                                        Training.Visible = true;
                                        InfoLabel.Text = nowbuilding.Resource;
                                        InfoValue.Text = nowbuilding.ResourceValue.ToString() + "%";
                                    }
                                    else
                                    {
                                        InfoLabel.Text = "Extracted " + nowbuilding.Resource;
                                        InfoValue.Text = nowbuilding.ResourceValue.ToString();
                                    }
                                }
                            }
                            Information.Visible = true;
                        }
                    }
                    for (int i = 0; i < ActiveElements.Count; ++i)
                    {
                        if (iX >= ActiveElements[i].iX && iX < ActiveElements[i].iX + ActiveElements[i].Size && iY >= ActiveElements[i].iY && iY < ActiveElements[i].iY + ActiveElements[i].Size)
                        {
                            SelectedPoint = new Point(ActiveElements[i].iX, ActiveElements[i].iY);
                            SelectedSize = ActiveElements[i].Size;
                            SelectedUnit = ActiveElements[i] as Unit;
                            if (ActiveElements[i].BlockIndice == 9)
                            {
                                Object r = Properties.Resources.ResourceManager.GetObject("FootManIcon");
                                Portrait.Image = (Bitmap)r;
                                NameOfBlock.Text = "Footman";
                                ShowUnitAction();
                            }
                            else if (ActiveElements[i].BlockIndice == 10)
                            {
                                Object r = Properties.Resources.ResourceManager.GetObject("ArcgerIcon");
                                Portrait.Image = (Bitmap)r;
                                NameOfBlock.Text = "Elven Archer";
                                ShowUnitAction();
                            }
                            LevelOfBlock.Text = ActiveElements[i].Lvl.ToString();
                            HpOfBlock.Text = ActiveElements[i].Hp.ToString() + " / " + ActiveElements[i].MaxHp;
                            Bitmap bmp = new Bitmap(progressHP.Width, progressHP.Height);
                            using (Graphics g = Graphics.FromImage(bmp))
                            {
                                g.Clear(Color.White);
                                float hpwidth = (float)ActiveElements[i].Hp / ActiveElements[i].MaxHp;
                                g.FillRectangle(Brushes.ForestGreen, new Rectangle(0, 0, (int)(hpwidth * progressHP.Width), progressHP.Height));
                            }
                            progressHP.Image = bmp;
                            progressHP.Refresh();
                            if (ActiveElements[i].Type == "Unit")
                            {
                                Unit nowunit = ActiveElements[i] as Unit;
                                InfoLabel.Text = "Damage";
                                InfoValue.Text = nowunit.AttackDamage.ToString();
                            }
                            Information.Visible = true;
                        }
                    }
                }
            }
            pictureBox1.Refresh();
            miniMap.Refresh();
        }

        private void PictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            IsPressed = false;
        }

        private void PictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            nowPoint = new Point(e.X - ((int)MapBitmap.Width / 2 - (int)MapBitmap.Width / 6 + shiftx + e.X) % 32, e.Y - ((int)MapBitmap.Width / 2 - (int)MapBitmap.Width / 6 + shifty + e.Y) % 32);
            if ((Creation && InMap) || (Delete && InMap) || (Moving && InMap) || Extract && InMap)
            {
                pictureBox1.Refresh();
            }
        }

        private void PictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            IsPressed = true;
        }

        private void MiniMap_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImage(MapBitmap, new Rectangle(0, 0, miniMap.Width, miniMap.Height), new Rectangle(0, 0, MapBitmap.Width, MapBitmap.Height), GraphicsUnit.Pixel);
            for (int i = 0; i < ActiveElements.Count; ++i)
            {
                if (ActiveElements[i].Type == "Unit")
                {
                    Unit nowUnit = ActiveElements[i] as Unit;
                    g.DrawImage(nowUnit.Frame, new Rectangle((int)nowUnit.realPoint.X / 8, (int)nowUnit.realPoint.Y / 8, 4, 4));
                }
            }
            for (int i = 0; i < Enemies.Count; ++i)
            {
                g.DrawImage(Enemies[i].Frame, new Rectangle((int)Enemies[i].realPoint.X / 8, (int)Enemies[i].realPoint.Y / 8, 4, 4));
            }
            g.DrawRectangle(new Pen(Brushes.Gold, 5), new Rectangle(0, 0, miniMap.Width, miniMap.Height));
            g.DrawRectangle(new Pen(Brushes.White, 1), new Rectangle((int)miniMap.Width / 2 - (int)miniMap.Width / 6 + shiftx / 8, (int)miniMap.Width / 2 - (int)miniMap.Width / 6 + shifty / 8, (int)miniMap.Width / 3, (int)miniMap.Width / 3));
        }

        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImage(MapBitmap, 0, 0, new Rectangle((int)MapBitmap.Width / 2 - (int)MapBitmap.Width / 6 + shiftx, (int)MapBitmap.Width / 2 - (int)MapBitmap.Width / 6 + shifty, (int)MapBitmap.Width / 3, (int)MapBitmap.Width / 3), GraphicsUnit.Pixel);
            for(int i = 0; i < ActiveElements.Count; ++i)
            {
                int oX = (int)MapBitmap.Width / 2 - (int)MapBitmap.Width / 6 + shiftx;
                int oY = (int)MapBitmap.Width / 2 - (int)MapBitmap.Width / 6 + shifty;
                if (ActiveElements[i].Type == "Unit")
                {
                    Unit nowUnit = ActiveElements[i] as Unit;
                    g.DrawImage(nowUnit.Frame, new Rectangle((int)nowUnit.realPoint.X - oX, (int)nowUnit.realPoint.Y - oY, 32, 32));
                }
            }
            for(int i = 0; i < Enemies.Count; ++i)
            {
                int oX = (int)MapBitmap.Width / 2 - (int)MapBitmap.Width / 6 + shiftx;
                int oY = (int)MapBitmap.Width / 2 - (int)MapBitmap.Width / 6 + shifty;
                g.DrawImage(Enemies[i].Frame, new Rectangle((int)Enemies[i].realPoint.X - oX, (int)Enemies[i].realPoint.Y - oY, 32, 32));
            }
            if (Creation)
            {
                int oX = (int)MapBitmap.Width / 2 - (int)MapBitmap.Width / 6 + shiftx;
                int oY = (int)MapBitmap.Width / 2 - (int)MapBitmap.Width / 6 + shifty;
                for (int i = 0; i < AllBuildings.Count; ++i)
                {
                    g.DrawRectangle(new Pen(Brushes.AntiqueWhite, 2), new Rectangle(AllBuildings[i].iX * 32 - oX, AllBuildings[i].iY * 32 - oY, AllBuildings[i].Size * 32, AllBuildings[i].Size * 32));
                }
            }
            if (Creation && InMap)
            {
                int iX = ((int)MapBitmap.Width / 2 - (int)MapBitmap.Width / 6 + shiftx + nowPoint.X) / 32;
                int iY = ((int)MapBitmap.Width / 2 - (int)MapBitmap.Width / 6 + shifty + nowPoint.Y) / 32;
                if(CreationIndice == 1)
                    UpdateTexture(iX, iY);
                g.DrawImage(nowBuilding, new Rectangle(nowPoint.X, nowPoint.Y, 32*CreationIndice, 32*CreationIndice));
                if (iX <= 63 && iY <= 63 && iX >= 0 && iY >= 0)
                {
                    bool busy = false;
                    for(int i = iX; i < iX + CreationIndice; i++)
                    {
                        for(int j = iY; j < iY + CreationIndice; j++)
                        {
                           if(Blocks[i, j] != 0)
                            {
                                busy = true;
                                break;
                            }
                        }
                        if (busy == true)
                            break;
                    }
                    if (busy)
                    {
                        g.FillRectangle(new SolidBrush(Color.FromArgb(180, Color.DarkRed)), new Rectangle(nowPoint.X, nowPoint.Y, nowBuilding.Width, nowBuilding.Height));
                    }
                    else
                    {
                        g.FillRectangle(new SolidBrush(Color.FromArgb(180, Color.ForestGreen)), new Rectangle(nowPoint.X, nowPoint.Y, nowBuilding.Width, nowBuilding.Height));
                    }
                }
            }
            if (Delete)
            {
                int oX = (int)MapBitmap.Width / 2 - (int)MapBitmap.Width / 6 + shiftx;
                int oY = (int)MapBitmap.Width / 2 - (int)MapBitmap.Width / 6 + shifty;
                for (int i = 0; i < ActiveElements.Count; ++i)
                {
                    if (ActiveElements[i].BlockIndice == 5)
                        g.DrawRectangle(new Pen(Brushes.DarkRed, 2), new Rectangle(ActiveElements[i].iX * 32 - oX, ActiveElements[i].iY * 32 - oY, ActiveElements[i].Size * 32, ActiveElements[i].Size * 32));
                }
            }
            if (Delete && InMap)
            {
                int iX = ((int)MapBitmap.Width / 2 - (int)MapBitmap.Width / 6 + shiftx + nowPoint.X) / 32;
                int iY = ((int)MapBitmap.Width / 2 - (int)MapBitmap.Width / 6 + shifty + nowPoint.Y) / 32;
                g.DrawRectangle(new Pen(Brushes.White, 2), new Rectangle(nowPoint.X, nowPoint.Y, nowBuilding.Width, nowBuilding.Height));
                if(iX <= 63 && iY <= 63 && iX >=0 && iY >= 0)
                {
                    if (Blocks[iX, iY] == 5 || Blocks[iX, iY] == 5)
                    {
                        g.FillRectangle(new SolidBrush(Color.FromArgb(180, Color.DarkRed)), new Rectangle(nowPoint.X, nowPoint.Y, nowBuilding.Width, nowBuilding.Height));
                    }
                }
                
            }
            if (Moving && InMap)
            {
                /*int iX = ((int)MapBitmap.Width / 2 - (int)MapBitmap.Width / 6 + shiftx + nowPoint.X) / 32;
                int iY = ((int)MapBitmap.Width / 2 - (int)MapBitmap.Width / 6 + shifty + nowPoint.Y) / 32;
                int oX = (int)MapBitmap.Width / 2 - (int)MapBitmap.Width / 6 + shiftx;
                int oY = (int)MapBitmap.Width / 2 - (int)MapBitmap.Width / 6 + shifty;
                Point EndPoint = new Point(iX, iY);
                List<Point> path = FindPath(Blocks, SelectedPoint, EndPoint);
                if (path == null)
                {
                    g.FillRectangle(new SolidBrush(Color.FromArgb(180, Color.DarkRed)), new Rectangle(nowPoint.X, nowPoint.Y, 32, 32));
                    g.DrawRectangle(new Pen(Brushes.White, 1), new Rectangle(nowPoint.X, nowPoint.Y, 32, 32));
                }
                else
                {
                    if (path.Last() != EndPoint)
                    {
                        g.FillRectangle(new SolidBrush(Color.FromArgb(180, Color.LightYellow)), new Rectangle(path.Last().X * 32 - oX, path.Last().Y * 32 - oY, 32, 32));
                        g.FillRectangle(new SolidBrush(Color.FromArgb(180, Color.LightYellow)), new Rectangle(path.Last().X * 32 - oX, path.Last().Y * 32 - oY, 32, 32));
                        g.DrawRectangle(new Pen(Brushes.White, 1), new Rectangle(nowPoint.X, nowPoint.Y, 32, 32));
                    }
                    else
                    {
                        g.FillRectangle(new SolidBrush(Color.FromArgb(180, Color.ForestGreen)), new Rectangle(nowPoint.X, nowPoint.Y, 32, 32));
                        g.DrawRectangle(new Pen(Brushes.White, 1), new Rectangle(nowPoint.X, nowPoint.Y, 32, 32));
                    }
                    for (int i = 1; i < path.Count; ++i)
                    {
                        g.DrawLine(new Pen(Brushes.White, 1), new PointF(path[i - 1].X * 32 + 16 - oX, path[i - 1].Y * 32 + 16 - oY), new PointF(path[i].X * 32 + 16 - oX, path[i].Y * 32 + 16 - oY));
                    }
                }*/
                g.DrawRectangle(new Pen(Brushes.White, 1), new Rectangle(nowPoint.X, nowPoint.Y, 32, 32));
            }
            if (Extract)
            {
                int oX = (int)MapBitmap.Width / 2 - (int)MapBitmap.Width / 6 + shiftx;
                int oY = (int)MapBitmap.Width / 2 - (int)MapBitmap.Width / 6 + shifty;
                for (int i = 0; i < AllBuildings.Count; ++i)
                {
                    if (AllBuildings[i].BlockIndice >= 2 && AllBuildings[i].BlockIndice <= 4)
                    {
                        Building building = AllBuildings[i] as Building;
                        if(building.ResourceValue == 0)
                        {
                            g.DrawRectangle(new Pen(Brushes.DarkSlateGray, 2), new Rectangle(AllBuildings[i].iX * 32 - oX, AllBuildings[i].iY * 32 - oY, AllBuildings[i].Size * 32, AllBuildings[i].Size * 32));
                        }
                        
                    }
                        
                }
            }
            if (Extract && InMap)
            {
                int iX = ((int)MapBitmap.Width / 2 - (int)MapBitmap.Width / 6 + shiftx + nowPoint.X) / 32;
                int iY = ((int)MapBitmap.Width / 2 - (int)MapBitmap.Width / 6 + shifty + nowPoint.Y) / 32;
                if( Math.Abs(SelectedPoint.X - iX) <= 1 && Math.Abs(SelectedPoint.Y - iY) <= 1 && !(SelectedPoint.X == iX && SelectedPoint.Y == iY))
                {
                    g.DrawRectangle(new Pen(Brushes.White, 1), new Rectangle(nowPoint.X, nowPoint.Y, 32, 32));
                }
            }
            if(!Creation && !Delete && !Moving && !Extract)
            {
                if(SelectedSize > 0)
                {
                    int oX = (int)MapBitmap.Width / 2 - (int)MapBitmap.Width / 6 + shiftx;
                    int oY = (int)MapBitmap.Width / 2 - (int)MapBitmap.Width / 6 + shifty;
                    if (!ListOfMovingCharacters.Contains(SelectedPoint) && Blocks[SelectedPoint.X, SelectedPoint.Y] != 0)
                    {
                        g.DrawRectangle(new Pen(Brushes.LightGreen, 1), new Rectangle(SelectedPoint.X * 32 - oX, SelectedPoint.Y * 32 - oY, SelectedSize * 32, SelectedSize * 32));
                    }
                }
            }
        }

        private void MiniMap_MouseUp(object sender, MouseEventArgs e)
        {
            IsPressed = false;
            MiniMap_timer.Stop();
        }

        private void MiniMap_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsPressed)
            {
                int NowX = e.X;
                int NowY = e.Y;
                int min = 256 / (3 * 2) + 1;
                int max = 256 - min;
                if (NowX < min)
                    NowX = min;
                if (NowY < min)
                    NowY = min;
                if (NowX > max)
                    NowX = max;
                if (NowY > max)
                    NowY = max;
                shiftx += (NowX - OldX) * 8;
                shifty += (NowY - OldY) * 8;
                /*if (OldY != NowY && OldX != NowX)
                    DrawMap();*/
                OldX = NowX;
                OldY = NowY;
            }
        }

        private void MiniMap_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                IsPressed = true;
                int NowX = e.X;
                int NowY = e.Y;
                int min = 256 / (3 * 2) + 1;
                int max = 256 - min;
                if(NowX < min)
                    NowX = min;
                if (NowY < min)
                    NowY = min;
                if (NowX > max)
                    NowX = max;
                if (NowY > max)
                    NowY = max;
                shiftx += (NowX - OldX) * 8;
                shifty += (NowY - OldY) * 8;
                MiniMap_timer.Start();
                /*DrawMap();*/
                OldX = NowX;
                OldY = NowY;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            int Count = 3;
            switch (keyData)
            {
                case (Keys.Up):
                    if (shifty > -(int)MapBitmap.Width / Count && shifty < -((int)MapBitmap.Width / Count - 32))
                        shifty -= (shifty + (int)MapBitmap.Width / Count);
                    else if (shifty > -((int)MapBitmap.Width / Count - 32))
                        shifty -= 32;
                    DrawMap();
                    return true;
                case (Keys.Down):
                    
                    if (shifty < (int)MapBitmap.Width / Count && shifty > ((int)MapBitmap.Width / Count - 32))
                        shifty += ((int)MapBitmap.Width / Count - shifty);
                    else if (shifty < ((int)MapBitmap.Width / Count - 32))
                        shifty += 32;
                    DrawMap();
                    return true;
                case (Keys.Left):
                    if (shiftx > -(int)MapBitmap.Width / Count && shiftx < -((int)MapBitmap.Width / Count - 32))
                        shiftx -= (shiftx + (int)MapBitmap.Width / Count);
                    else if (shiftx > -((int)MapBitmap.Width / Count - 32))
                        shiftx -= 32;
                    DrawMap();
                    return true;
                case (Keys.Right):
                    if (shiftx < (int)MapBitmap.Width / Count && shiftx > ((int)MapBitmap.Width / Count - 32))
                        shiftx += ((int)MapBitmap.Width / Count - shiftx);
                    else if (shiftx < ((int)MapBitmap.Width / Count - 32))
                        shiftx += 32;
                    DrawMap();
                    return true;
                default:
                    break;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void DrawMap()
        {
            pictureBox1.Refresh();
            miniMap.Refresh();
            OldX = ((int)MapBitmap.Width / 2 - (int)MapBitmap.Width / 6 + (int)MapBitmap.Width / 6 + shiftx) / 8;
            OldY = ((int)MapBitmap.Width / 2 - (int)MapBitmap.Width / 6 + (int)MapBitmap.Width / 6 + shifty) / 8;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Width = MapBitmap.Width;
            pictureBox1.Height = MapBitmap.Height;
            for (int i = 0; i < 64; ++i)
            {
                for (int j = 0; j < 64; ++j)
                {
                    if (i == 0 || i == 1 || i == 62 || i == 63 || j == 0 || j == 1 || j == 62 || j == 63)
                    {
                        Blocks[i, j] = 1;
                    }
                    else
                    {
                        Blocks[i, j] = 0;
                    }
                }
            }
            using (Graphics g = Graphics.FromImage(MapBitmap))
            {
                g.FillRectangle(Brushes.White, 0, 0, MapBitmap.Width, MapBitmap.Height);
                for (int y = 0; y < MapSize /*2*/; y++)
                {
                    for (int x = 0; x < MapSize /*2*/; x++)
                    {
                        /*int num = rnd.Next(239, 271);
                        Object rm = Properties.Resources.ResourceManager.GetObject("_" + num.ToString());*/
                        Object rm = Properties.Resources.ResourceManager.GetObject("_512x512");
                        if (x == 0 && y == 0)
                            rm = Properties.Resources.ResourceManager.GetObject("_0_1");
                        else if (x == MapSize - 1 && y == 0)
                            rm = Properties.Resources.ResourceManager.GetObject("_0_2");
                        else if (x == 0 && y == MapSize - 1)
                            rm = Properties.Resources.ResourceManager.GetObject("_0_3");
                        else if (x == MapSize - 1 && y == MapSize - 1)
                            rm = Properties.Resources.ResourceManager.GetObject("_0_4");
                        else if (x > 0 && x < MapSize - 1 && y == 0)
                            rm = Properties.Resources.ResourceManager.GetObject("_0_5");
                        else if (x > 0 && x < MapSize - 1 && y == MapSize - 1)
                            rm = Properties.Resources.ResourceManager.GetObject("_0_6");
                        else if (x == 0 && y > 0 && y < MapSize - 1)
                            rm = Properties.Resources.ResourceManager.GetObject("_0_7");
                        else if (x == MapSize - 1 && y > 0 && y < MapSize - 1)
                            rm = Properties.Resources.ResourceManager.GetObject("_0_8");
                        nowBitmap = new Bitmap(pictureBox1.Width / MapSize, pictureBox1.Width / MapSize);
                        nowBitmap = (Bitmap)rm;
                        g.DrawImage(nowBitmap, new Rectangle(x * pictureBox1.Width / MapSize/*512*/, y * pictureBox1.Width / MapSize/* 512*/, pictureBox1.Width / MapSize/* 512*/, pictureBox1.Width / MapSize /*512*/));
                        /*nowBitmap.Dispose();*/
                    }
                }
                Object r = Properties.Resources.ResourceManager.GetObject("_1_7");
                Bitmap mineshaft = (Bitmap)r;
                mineshaft.MakeTransparent(mineshaft.GetPixel(0, 0));
                r = Properties.Resources.ResourceManager.GetObject("_1_4");
                Bitmap farm = (Bitmap)r;
                farm.MakeTransparent(farm.GetPixel(0, 0));
                r = Properties.Resources.ResourceManager.GetObject("_1_9");
                Bitmap stone = (Bitmap)r;
                stone.MakeTransparent(stone.GetPixel(0, 0));
                r = Properties.Resources.ResourceManager.GetObject("_1_10");
                Bitmap pentagram = (Bitmap)r;
                pentagram.MakeTransparent(pentagram.GetPixel(0, 0));
                g.DrawImage(mineshaft, 1024 - 64, 1024 - 64, new Rectangle(0, 0, mineshaft.Width, mineshaft.Height), GraphicsUnit.Pixel);
                int iX = (1024 - 64) / 32;
                int iY = (1024 - 64) / 32;
                Building newBuilding = new Building(iX, iY, 3, 3, 1, 200, 200, "Building", "Gold", 0, false);
                AllBuildings.Add(newBuilding);
                for (int i = 30; i < 33; ++i)
                {
                    for (int j = 30; j < 33; ++j)
                    {
                        Blocks[i, j] = 3;
                    }
                }
                for (int i = 2; i < 4; ++i)
                {
                    for (int j = 2; j < 4; ++j)
                    {
                        Blocks[i, j] = 7;

                    }
                }
                g.DrawImage(pentagram, 2 * 32, 2 * 32, new Rectangle(0, 0, pentagram.Width, pentagram.Height), GraphicsUnit.Pixel);
                for (int i = 59; i < 61; ++i)
                {
                    for (int j = 59; j < 61; ++j)
                    {
                        Blocks[i, j] = 7;
                    }
                }
                g.DrawImage(pentagram, 59 * 32, 59 * 32, new Rectangle(0, 0, pentagram.Width, pentagram.Height), GraphicsUnit.Pixel);
                Random rnd = new Random();
                for (int c = 0; c < 15; ++c)
                {
                    int x;
                    int y;
                    int ForestIndex = rnd.Next(1, 7);
                    r = Properties.Resources.ResourceManager.GetObject("f_" + ForestIndex.ToString());
                    Bitmap nowforest = (Bitmap)r;
                    while (true)
                    {
                        x = rnd.Next(3, 53);
                        y = rnd.Next(3, 53);
                        CanBuild = true;
                        for (int i = x; i < x + nowforest.Width / 32; ++i)
                        {
                            for (int j = y; j < y + nowforest.Height / 32; ++j)
                            {
                                if (Blocks[i, j] != 0)
                                {
                                    CanBuild = false;
                                    break;
                                }

                            }
                        }
                        if (CanBuild)
                            break;
                    }
                    switch (ForestIndex)
                    {
                        case 1:
                            for (int i = x; i < x + nowforest.Width / 32; ++i)
                            {
                                for (int j = y; j < y + nowforest.Height / 32; ++j)
                                {
                                    Blocks[i, j] = 1;
                                }
                            }
                            break;
                        case 2:
                            for (int i = x; i < x + nowforest.Width / 32; ++i)
                            {
                                for (int j = y; j < y + nowforest.Height / 32; ++j)
                                {
                                    if (((i == x) && (j == y || y == y + 1)) || ((i == x + 1) && (j == y)) || ((i == x + nowforest.Width / 32 - 2) && (j == y + nowforest.Height / 32 - 1)) || ((i == x + nowforest.Width / 32 - 1) && (j == y + nowforest.Height / 32 - 2 || j == y + nowforest.Height / 32 - 1)))
                                        Blocks[i, j] = 0;
                                    else
                                        Blocks[i, j] = 1;
                                }
                            }
                            break;
                        case 3:
                            for (int i = x; i < x + nowforest.Width / 32; ++i)
                            {
                                for (int j = y; j < y + nowforest.Height / 32; ++j)
                                {
                                    if (((i == x) && (j == y + nowforest.Height / 32 - 2 || j == y + nowforest.Height / 32 - 1)) || ((i == x + 1) && (j == y + nowforest.Height / 32 - 1)) || ((i == x + nowforest.Width / 32 - 1) && (j == y)) || ((i == x + nowforest.Width / 32 - 2) && (j == y || j == y + 1)))
                                        Blocks[i, j] = 0;
                                    else
                                        Blocks[i, j] = 1;
                                }
                            }
                            break;
                        case 4:
                            for (int i = x; i < x + nowforest.Width / 32; ++i)
                            {
                                for (int j = y; j < y + nowforest.Height / 32; ++j)
                                {
                                    if (((i >= x + 3 && i <= x + nowforest.Width / 32 - 1) && (j == y)) || ((i == x + nowforest.Width / 32 - 1) && (j == y + 1)) || ((i >= x && i <= x + 4) && (j == y + nowforest.Height / 32 - 1)) || ((i == x) && (j == y + nowforest.Height / 32 - 2)))
                                        Blocks[i, j] = 0;
                                    else
                                        Blocks[i, j] = 1;
                                }
                            }
                            break;
                        case 5:
                            for (int i = x; i < x + nowforest.Width / 32; ++i)
                            {
                                for (int j = y; j < y + nowforest.Height / 32; ++j)
                                {
                                    if (((i == x) && (j == y)) || ((i == x + nowforest.Width / 32 - 1) && (j == y)) || ((i == x) && (j == y + nowforest.Height / 32 - 1)) || ((i == x + nowforest.Width / 32 - 1) && (j == y + nowforest.Height / 32 - 1)))
                                        Blocks[i, j] = 0;
                                    else
                                        Blocks[i, j] = 1;
                                }
                            }
                            break;
                        case 6:
                            for (int i = x; i < x + nowforest.Width / 32; ++i)
                            {
                                for (int j = y; j < y + nowforest.Height / 32; ++j)
                                {
                                    if (((i == x + nowforest.Width / 32 - 2) && (j == y)) || ((i == x + nowforest.Width / 32 - 1) && (j == y || j == y + 1 || j == y + 2)) || ((i == x) && (j >= y + 4 && j <= y + nowforest.Height / 32 - 1)) || ((i == x + 1) && (j == y + nowforest.Height / 32 - 1 || j == y + nowforest.Height / 32 - 2)))
                                        Blocks[i, j] = 0;
                                    else
                                        Blocks[i, j] = 1;
                                }
                            }
                            break;
                    }
                    g.DrawImage(nowforest, x * 32, y * 32, new Rectangle(0, 0, nowforest.Width, nowforest.Height), GraphicsUnit.Pixel);
                }
                for (int CoMx = 3; CoMx < 61; CoMx += 32)
                {
                    int x;
                    int y;
                    while (true)
                    {
                        x = rnd.Next(CoMx, CoMx + 20);
                        y = rnd.Next(3, 59);
                        CanBuild = true;
                        if (x > 27 && x < 38)
                        {
                            CanBuild = false;
                        }
                        else
                        {
                            for (int i = x; i < x + 3; ++i)
                            {
                                for (int j = y; j < y + 3; ++j)
                                {
                                    if (Blocks[i, j] != 0)
                                    {
                                        CanBuild = false;
                                        break;
                                    }

                                }
                            }
                        }
                        if (CanBuild)
                            break;
                    }
                    newBuilding = new Building(x, y, 3, 3, 1, 200, 200, "Building", "Gold", 0, false);
                    AllBuildings.Add(newBuilding);
                    for (int i = x; i < x + 3; ++i)
                    {
                        for (int j = y; j < y + 3; ++j)
                        {
                            Blocks[i, j] = 3;
                        }
                    }
                    g.DrawImage(mineshaft, x * 32, y * 32, new Rectangle(0, 0, mineshaft.Width, mineshaft.Height), GraphicsUnit.Pixel);
                    
                }
                for (int c = 0; c < 5; ++c)
                {
                    int x;
                    int y;
                    while (true)
                    {
                        x = rnd.Next(3, 60);
                        y = rnd.Next(3, 60);
                        CanBuild = true;
                        for (int i = x; i < x + 2; ++i)
                        {
                            for (int j = y; j < y + 2; ++j)
                            {
                                if (Blocks[i, j] != 0)
                                {
                                    CanBuild = false;
                                    break;
                                }

                            }
                        }
                        if (CanBuild)
                            break;
                    }
                    newBuilding = new Building(x, y, 2, 2, 1, 50, 50, "Building", "Food", 0, false);
                    AllBuildings.Add(newBuilding);
                    for (int i = x; i < x + 2; ++i)
                    {
                        for (int j = y; j < y + 2; ++j)
                        {
                            Blocks[i, j] = 2;
                        }
                    }
                    g.DrawImage(farm, x * 32, y * 32, new Rectangle(0, 0, farm.Width, farm.Height), GraphicsUnit.Pixel);
                }
                for (int c = 0; c < 5; ++c)
                {
                    int x;
                    int y;
                    while (true)
                    {
                        x = rnd.Next(3, 60);
                        y = rnd.Next(3, 60);
                        CanBuild = true;
                        for (int i = x; i < x + 2; ++i)
                        {
                            for (int j = y; j < y + 2; ++j)
                            {
                                if (Blocks[i, j] != 0)
                                {
                                    CanBuild = false;
                                    break;
                                }

                            }
                        }
                        if (CanBuild)
                            break;
                    }
                    newBuilding = new Building(x, y, 4, 2, 1, 80, 80, "Building", "Stone", 0, false);
                    AllBuildings.Add(newBuilding);
                    for (int i = x; i < x + 2; ++i)
                    {
                        for (int j = y; j < y + 2; ++j)
                        {
                            Blocks[i, j] = 4;
                        }
                    }
                    g.DrawImage(stone, x * 32, y * 32, new Rectangle(0, 0, stone.Width, stone.Height), GraphicsUnit.Pixel);
                }
            }
            miniMap.Width = 256;
            miniMap.Height = 256;
            pictureBox1.Width = 682;
            pictureBox1.Height = 682;
            miniMap.SizeMode = PictureBoxSizeMode.StretchImage;
            DrawMap();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DrawMap();
        }

        private void CreateWalls_Click(object sender, EventArgs e)
        {
            if (NowWood >= 3 && NowStone >= 1)
            {
                Information.Visible = false;
                CreationIndice = 1;
                Creation = true;
                Delete = false;
                Moving = false;
                Extract = false;
                Object r = Properties.Resources.ResourceManager.GetObject("_17");
                nowBuilding = (Bitmap)r;
                NecessaryResources.Visible = true;
                HideAllIcons();
                CancelAction.Visible = true;
                pictureBox1.Refresh();
            }
            else
            {
                InGameMessage.Visible = true;
                InGameMessage.Text = "You don't have enough resources";
                MessageTimer.Stop();
                MessageTimer.Start();
            }
        }

        private void DeleteWalls_Click(object sender, EventArgs e)
        {
            Information.Visible = false;
            Delete = true;
            Creation = false;
            Moving = false;
            Extract = false;
            HideAllIcons();
            Object r = Properties.Resources.ResourceManager.GetObject("_17");
            nowBuilding = (Bitmap)r;
            CancelAction.Visible = true;
            pictureBox1.Refresh();
        }

        private void CreateTownHall_Click(object sender, EventArgs e)
        {
            if(NowGold >= 400 && NowStone >= 16)
            {
                Information.Visible = false;
                CreationIndice = 4;
                Creation = true;
                Delete = false;
                Moving = false;
                Extract = false;
                Object th = Properties.Resources.ResourceManager.GetObject("_1_3");
                nowBuilding = (Bitmap)th;
                HideAllIcons();
                CancelAction.Visible = true;
                pictureBox1.Refresh();
            }
            else
            {
                InGameMessage.Visible = true;
                InGameMessage.Text = "You don't have enough resources";
                MessageTimer.Stop();
                MessageTimer.Start();
            }
            
        }

        private void CreateTownHall_MouseEnter(object sender, EventArgs e)
        {
            Object th = Properties.Resources.ResourceManager.GetObject("StoneIcon");
            pictureBox7.Image = (Bitmap)th;
            th = Properties.Resources.ResourceManager.GetObject("GoldIcon");
            pictureBox8.Image = (Bitmap)th;
            NessRes1.Text = "16";
            NessRes2.Text = "400";
            NameOfButton.Text = "Create Town Hall";
            NecessaryResources.Visible = true;
        }

        private void CreateTownHall_MouseLeave(object sender, EventArgs e)
        {
            NecessaryResources.Visible = false;
        }

        private void UpTownHall_MouseEnter(object sender, EventArgs e)
        {
            Object th = Properties.Resources.ResourceManager.GetObject("StoneIcon");
            pictureBox7.Image = (Bitmap)th;
            th = Properties.Resources.ResourceManager.GetObject("GoldIcon");
            pictureBox8.Image = (Bitmap)th;
            NessRes1.Text = "10";
            NessRes2.Text = "400";
            NameOfButton.Text = "Up Town Hall";
            NecessaryResources.Visible = true;
        }

        private void UpTownHall_MouseLeave(object sender, EventArgs e)
        {
            NecessaryResources.Visible = false;
        }

        private void CreateBarracks_MouseEnter(object sender, EventArgs e)
        {
            Object th = Properties.Resources.ResourceManager.GetObject("StoneIcon");
            pictureBox7.Image = (Bitmap)th;
            th = Properties.Resources.ResourceManager.GetObject("GoldIcon");
            pictureBox8.Image = (Bitmap)th;
            NessRes1.Text = "5";
            NessRes2.Text = "200";
            NameOfButton.Text = "Create Barracks";
            NecessaryResources.Visible = true;
        }

        private void CreateBarracks_MouseLeave(object sender, EventArgs e)
        {
            NecessaryResources.Visible = false;
        }

        private void CreateFarm_MouseEnter(object sender, EventArgs e)
        {
            Object th = Properties.Resources.ResourceManager.GetObject("StoneIcon");
            pictureBox7.Image = (Bitmap)th;
            th = Properties.Resources.ResourceManager.GetObject("WoodIcon");
            pictureBox8.Image = (Bitmap)th;
            NessRes1.Text = "3";
            NessRes2.Text = "40";
            NameOfButton.Text = "Create Farm";
            NecessaryResources.Visible = true;
        }

        private void CreateFarm_MouseLeave(object sender, EventArgs e)
        {
            NecessaryResources.Visible = false;
        }

        private void CreateWalls_MouseEnter(object sender, EventArgs e)
        {
            Object th = Properties.Resources.ResourceManager.GetObject("StoneIcon");
            pictureBox7.Image = (Bitmap)th;
            th = Properties.Resources.ResourceManager.GetObject("WoodIcon");
            pictureBox8.Image = (Bitmap)th;
            NessRes1.Text = "1";
            NessRes2.Text = "3";
            NameOfButton.Text = "Create Walls";
            NecessaryResources.Visible = true;
        }

        private void CreateWalls_Leave(object sender, EventArgs e)
        {
            NecessaryResources.Visible = false;
        }

        private void CreateFootMan_MouseEnter(object sender, EventArgs e)
        {
            Object th = Properties.Resources.ResourceManager.GetObject("GoldIcon");
            pictureBox7.Image = (Bitmap)th;
            th = Properties.Resources.ResourceManager.GetObject("FoodIcon");
            pictureBox8.Image = (Bitmap)th;
            NessRes1.Text = "50";
            NessRes2.Text = "5";
            NameOfButton.Text = "Footman";
            NecessaryResources.Visible = true;
        }

        private void CreateFootMan_MouseLeave(object sender, EventArgs e)
        {
            NecessaryResources.Visible = false;
        }

        private void CreateArcher_MouseEnter(object sender, EventArgs e)
        {
            Object th = Properties.Resources.ResourceManager.GetObject("GoldIcon");
            pictureBox7.Image = (Bitmap)th;
            th = Properties.Resources.ResourceManager.GetObject("FoodIcon");
            pictureBox8.Image = (Bitmap)th;
            NessRes1.Text = "60";
            NessRes2.Text = "4";
            NameOfButton.Text = "Elven Archer";
            NecessaryResources.Visible = true;
        }

        private void CreateArcher_MouseLeave(object sender, EventArgs e)
        {
            NecessaryResources.Visible = false;
        }

        private void CancelAction_Click(object sender, EventArgs e)
        {
            if(Creation || Delete || Moving || Extract)
            {
                if (Creation)
                    Creation = false;
                if(Delete) 
                    Delete = false;
                if (Moving)
                    Moving = false;
                if(Extract)
                    Extract = false;
            }
            CancelAction.Visible = false;
            bool haveth = false;
            for (int i = 0; i < 64; ++i)
            {
                for (int j = 0; j < 64; ++j)
                {
                    if (Blocks[i, j] == 6)
                    {
                        haveth = true;
                    }
                }
            }
            if(!haveth)
            {
                ShowTHVisible();
            }
            HideAllIcons();
            SelectedPoint = new Point(-1, -1);
            SelectedSize = 0;
            pictureBox1.Refresh();
            SelectedSize = 0;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            InGameMessage.Visible = false;
            MessageTimer.Stop();
        }

        private void UpTownHall_Click(object sender, EventArgs e)
        {
            if (NowGold >= 400 && NowStone >= 10)
            {
                using (Graphics g = Graphics.FromImage(MapBitmap))
                {
                    for(int x = 0; x < 64; ++x)
                    {
                        for(int y = 0; y < 64; ++y)
                        {
                            if (Blocks[x, y] == 6)
                            {
                                Object r = Properties.Resources.ResourceManager.GetObject("_01");
                                g.DrawImage((Bitmap)r, x * 32, y * 32);
                            }
                        }
                    }
                    for(int i = 0; i < ActiveElements.Count; ++i)
                    {
                        if (ActiveElements[i].BlockIndice == 6)
                        {
                            Object r = Properties.Resources.ResourceManager.GetObject("_1_2");
                            g.DrawImage((Bitmap)r, new Rectangle(ActiveElements[i].iX * 32, ActiveElements[i].iY * 32, 32 * 4, 32 * 4));
                            ActiveElements[i].Lvl++;

                        }
                    }
                }
                NowStone -= 10;
                StoneAmount.Text = NowStone.ToString();
                NowGold -= 400;
                GoldAmount.Text = NowGold.ToString();
                HideAllIcons();
                Information.Visible = false;
                pictureBox1.Refresh();
                InGameMessage.Visible = true;
                InGameMessage.Text = "The Town Hall has been improved! Archers are now available in the barracks";
                MessageTimer.Stop();
                MessageTimer.Start();
            }
            else
            {
                InGameMessage.Visible = true;
                InGameMessage.Text = "You don't have enough resources";
                MessageTimer.Stop();
                MessageTimer.Start();
            }
        }

        private void CreateBarracks_Click(object sender, EventArgs e)
        {
            if (NowGold >= 200 && NowStone >= 5)
            {
                Information.Visible = false;
                CreationIndice = 3;
                Creation = true;
                Delete = false;
                Moving = false;
                Extract = false;
                Object brcks = Properties.Resources.ResourceManager.GetObject("_1_6");
                nowBuilding = (Bitmap)brcks;
                HideAllIcons();
                CancelAction.Visible = true;
                pictureBox1.Refresh();
            }
            else
            {
                InGameMessage.Visible = true;
                InGameMessage.Text = "You don't have enough resources";
                MessageTimer.Stop();
                MessageTimer.Start();
            }
        }

        private void CreateFarm_Click(object sender, EventArgs e)
        {
            if (NowWood >= 40 && NowStone >= 3)
            {
                Information.Visible = false;
                CreationIndice = 2;
                Creation = true;
                Delete = false;
                Moving = false;
                Extract = false;
                Object frm = Properties.Resources.ResourceManager.GetObject("_1_4_1");
                nowBuilding = (Bitmap)frm;
                HideAllIcons();
                CancelAction.Visible = true;
                pictureBox1.Refresh();
            }
            else
            {
                InGameMessage.Visible = true;
                InGameMessage.Text = "You don't have enough resources";
                MessageTimer.Stop();
                MessageTimer.Start();
            }
        }

        private void CreateFootMan_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < AllBuildings.Count; ++i)
            {
                if(SelectedPoint.X == AllBuildings[i].iX && SelectedPoint.Y == AllBuildings[i].iY)
                {
                    Building SelectedBarrack = AllBuildings[i] as Building;
                    if (SelectedBarrack.Create)
                    {
                        InGameMessage.Visible = true;
                        InGameMessage.Text = "The barracks is already in use";
                        MessageTimer.Stop();
                        MessageTimer.Start();
                    }
                    else
                    {
                        if (NowFood >= 5 && NowGold >= 50)
                        {
                            TrainingIndice = 1;
                            progress = 0;
                            BarrackTimer.Start();
                            AllBuildings.RemoveAt(i);
                            SelectedBarrack.Create = true;
                            SelectedBarrack.Resource = "Training";
                            SelectedBarrack.ResourceValue = progress;
                            AllBuildings.Add(SelectedBarrack);
                            NowFood -= 5;
                            FoodAmount.Text = NowFood.ToString();
                            NowGold -= 50;
                            GoldAmount.Text = NowGold.ToString();
                            CreateFootMan.Enabled = false;
                            CreateArcher.Enabled = false;
                        }
                        else
                        {
                            InGameMessage.Visible = true;
                            InGameMessage.Text = "You don't have enough resources";
                            MessageTimer.Stop();
                            MessageTimer.Start();
                        }
                    }
                }
            }
        }

        private void CreateArcher_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < AllBuildings.Count; ++i)
            {
                if (SelectedPoint.X == AllBuildings[i].iX && SelectedPoint.Y == AllBuildings[i].iY)
                {
                    Building SelectedBarrack = AllBuildings[i] as Building;
                    if (SelectedBarrack.Create)
                    {
                        InGameMessage.Visible = true;
                        InGameMessage.Text = "The barracks is already in use";
                        MessageTimer.Stop();
                        MessageTimer.Start();
                    }
                    else
                    {
                        if (NowFood >= 4 && NowGold >= 60)
                        {
                            TrainingIndice = 2;
                            progress = 0;
                            BarrackTimer.Start();
                            AllBuildings.RemoveAt(i);
                            SelectedBarrack.Create = true;
                            SelectedBarrack.Resource = "Training";
                            SelectedBarrack.ResourceValue = progress;
                            AllBuildings.Add(SelectedBarrack);
                            NowFood -= 4;
                            FoodAmount.Text = NowFood.ToString();
                            NowGold -= 60;
                            GoldAmount.Text = NowGold.ToString();
                            CreateFootMan.Enabled = false;
                            CreateArcher.Enabled = false;
                        }
                        else
                        {
                            InGameMessage.Visible = true;
                            InGameMessage.Text = "You don't have enough resources";
                            MessageTimer.Stop();
                            MessageTimer.Start();
                        }
                    }
                }
            }

        }

        private void CreateWalls_MouseLeave(object sender, EventArgs e)
        {
            NecessaryResources.Visible = false;
        }

        private void BarrackTimer_Tick(object sender, EventArgs e)
        {
            progress += 1;
            for (int i = 0; i < AllBuildings.Count; i++)
            {
                if (AllBuildings[i].BlockIndice == 8)
                {
                    Building SelectedBarrack = AllBuildings[i] as Building;
                    AllBuildings.RemoveAt(i);
                    SelectedBarrack.ResourceValue = progress;
                    AllBuildings.Add(SelectedBarrack);
                }
            }
            for (int i = 0; i < AllBuildings.Count; i++)
            {
                if (AllBuildings[i].BlockIndice == 8)
                {
                    if (AllBuildings[i].iX == SelectedPoint.X && AllBuildings[i].iY == SelectedPoint.Y)
                    {
                        ShowInfo(i);
                    }
                }
            }
            if (progress == 100)
            {
                for (int i = 0; i < AllBuildings.Count; i++)
                {
                    if (AllBuildings[i].BlockIndice == 8)
                    {
                        Building SelectedBarrack = AllBuildings[i] as Building;
                        AllBuildings.RemoveAt(i);
                        SelectedBarrack.Create = false;
                        SelectedBarrack.Resource = "";
                        SelectedBarrack.ResourceValue = 0;
                        AllBuildings.Add(SelectedBarrack);
                        int x = AllBuildings[i].iX;
                        int y = AllBuildings[i].iY;
                        int distance = 1;
                        while (true)
                        {
                            bool notbusy = false;
                            for(int x1 = x - distance; x1 < x + AllBuildings[i].Size + distance; ++x1)
                            {
                                for (int y1 = y - distance; y1 < y + AllBuildings[i].Size + distance; ++y1)
                                {
                                    if (Blocks[x1, y1] == 0)
                                    {
                                        x = x1;
                                        y = y1;
                                        notbusy = true;
                                        break;
                                    }
                                }
                                if(notbusy)
                                    break;
                            }
                            if (notbusy)
                                break;
                            else
                                distance++;
                        }
                        if(TrainingIndice == 1)
                        {
                            Unit Footman = new Unit(x, y, 9, 1, 1, 20, 20, "Unit", 5, new PointF(x * 32, y * 32), 1, null, null);
                            Object r = Properties.Resources.ResourceManager.GetObject("footman_stand5");
                            Bitmap FootmanBMP = (Bitmap)r;
                            Footman.Frame = FootmanBMP;
                            ActiveElements.Add(Footman);
                            Blocks[x, y] = 9;
                        }
                        if(TrainingIndice == 2)
                        {
                            Unit ElvenArcher = new Unit(x, y, 10, 1, 1, 15, 15, "Unit", 10, new PointF(x * 32, y * 32), 5, null, null);
                            Object r = Properties.Resources.ResourceManager.GetObject("archer_stand5");
                            Bitmap ArcherBMP = (Bitmap)r;
                            ElvenArcher.Frame = ArcherBMP;
                            ActiveElements.Add(ElvenArcher);
                            Blocks[x, y] = 10;
                        }
                    }
                }
                for (int i = 0; i < AllBuildings.Count; i++)
                {
                    if (AllBuildings[i].BlockIndice == 8)
                    {
                        if (AllBuildings[i].iX == SelectedPoint.X && AllBuildings[i].iY == SelectedPoint.Y)
                        {
                            Training.Visible = false;
                            ShowInfo(i);
                        }
                    }
                }
                Training.Visible = false;
                InRange();
                BarrackTimer.Stop();
                CreateFootMan.Enabled = true;
                CreateArcher.Enabled = true;
            }
            pictureBox1.Refresh();
            miniMap.Refresh();
        }

        private void DeleteWalls_MouseEnter(object sender, EventArgs e)
        {
            pictureBox7.Image = null;
            pictureBox8.Image = null;
            NessRes1.Text = "";
            NessRes2.Text = "";
            NameOfButton.Text = "Delete Walls";
            NecessaryResources.Visible = true;
        }

        private void DeleteWalls_MouseLeave(object sender, EventArgs e)
        {
            NecessaryResources.Visible = false;
        }

        private void MoveUnit_Click(object sender, EventArgs e)
        {
            Moving = true;
            Creation = false;
            Delete = false;
            Extract = false;
            HideAllIcons();
            CancelAction.Visible = true;
            pictureBox1.Refresh();
        }

        private void Moving_Timer1_Tick(object sender, EventArgs e)
        {
            if(/*IndicesOfPathes[0] < 5 && */ListOfPathes[0].Count > 0)
            {
                for(int i = 0; i < ActiveElements.Count; ++i)
                {
                    if (ActiveElements[i].iX == ListOfMovingCharacters[0].X && ActiveElements[i].iY == ListOfMovingCharacters[0].Y && ActiveElements[i].Type == "Unit")
                    {
                        int side = 0;
                        Unit unit1 = ActiveElements[i] as Unit;
                        Point Nextpoint = ListOfPathes[0][0];
                        /*bool busy = false;
                        for(int j = 0; j < ActiveElements.Count; ++j)
                        {
                            if (ActiveElements[j].Type == "Unit")
                            {
                                Unit busyUnit = ActiveElements[j] as Unit;
                                if(busyUnit.realPoint.X - 2 <= unit1.realPoint.X && busyUnit.realPoint.X + 2 >= unit1.realPoint.X && busyUnit.realPoint.Y - 2 <= unit1.realPoint.Y && busyUnit.realPoint.Y + 2 >= unit1.realPoint.Y && unit1.iX != busyUnit.iX && unit1.iY != busyUnit.iY)
                                {
                                    busy = true;
                                }
                            }
                        }*/
                        if(unit1.BlockIndice == 10)
                        {
                            if ((Blocks[Nextpoint.X, Nextpoint.Y] == 0 || Blocks[Nextpoint.X, Nextpoint.Y] == 5) /*&& !busy*/)
                            {
                                if (unit1.iX == Nextpoint.X && unit1.iY != Nextpoint.Y)
                                {
                                    if (unit1.iY > Nextpoint.Y)
                                    {
                                        unit1.realPoint = new PointF(unit1.realPoint.X, unit1.realPoint.Y - 6.4f);
                                        side = 1;
                                    }
                                    else
                                    {
                                        unit1.realPoint = new PointF(unit1.realPoint.X, unit1.realPoint.Y + 6.4f);
                                        side = 5;
                                    }
                                }
                                else if (unit1.iY == Nextpoint.Y && unit1.iX != Nextpoint.X)
                                {
                                    if (unit1.iX > Nextpoint.X)
                                    {
                                        unit1.realPoint = new PointF(unit1.realPoint.X - 6.4f, unit1.realPoint.Y);
                                        side = -3;
                                    }
                                    else
                                    {
                                        unit1.realPoint = new PointF(unit1.realPoint.X + 6.4f, unit1.realPoint.Y);
                                        side = 3;
                                    }
                                }
                                else if (unit1.iX != Nextpoint.X && unit1.iY != Nextpoint.Y)
                                {
                                    if (unit1.iX > Nextpoint.X && unit1.iY > Nextpoint.Y)
                                    {
                                        unit1.realPoint = new PointF(unit1.realPoint.X - 6.4f, unit1.realPoint.Y - 6.4f);
                                        side = -2;
                                    }
                                    else if (unit1.iX > Nextpoint.X && unit1.iY < Nextpoint.Y)
                                    {
                                        unit1.realPoint = new PointF(unit1.realPoint.X - 6.4f, unit1.realPoint.Y + 6.4f);
                                        side = -4;
                                    }
                                    else if (unit1.iX < Nextpoint.X && unit1.iY > Nextpoint.Y)
                                    {
                                        unit1.realPoint = new PointF(unit1.realPoint.X + 6.4f, unit1.realPoint.Y - 6.4f);
                                        side = 2;
                                    }
                                    else if (unit1.iX < Nextpoint.X && unit1.iY < Nextpoint.Y)
                                    {
                                        unit1.realPoint = new PointF(unit1.realPoint.X + 6.4f, unit1.realPoint.Y + 6.4f);
                                        side = 4;
                                    }
                                }
                                IndicesOfPathes[0]++;
                                unit1.Frame = GetBMPofFrame(side, IndicesOfPathes[0], unit1.BlockIndice);
                                if (IndicesOfPathes[0] == 5)
                                {
                                    if (Blocks[unit1.iX, unit1.iY] == 11)
                                    {
                                        Blocks[unit1.iX, unit1.iY] = 5;
                                    }
                                    else
                                    {
                                        Blocks[unit1.iX, unit1.iY] = 0;
                                    }
                                    unit1.iX = Nextpoint.X;
                                    unit1.iY = Nextpoint.Y;
                                    unit1.realPoint = new Point(unit1.iX * 32, unit1.iY * 32);
                                    if (unit1.BlockIndice == 9)
                                        Blocks[Nextpoint.X, Nextpoint.Y] = 9;
                                    if (unit1.BlockIndice == 10)
                                    {
                                        if(Blocks[Nextpoint.X, Nextpoint.Y] == 5)
                                            Blocks[Nextpoint.X, Nextpoint.Y] = 11;
                                        else
                                            Blocks[Nextpoint.X, Nextpoint.Y] = 10;
                                    }
                                    ListOfMovingCharacters[0] = Nextpoint;
                                    IndicesOfPathes[0] = 0;
                                    ListOfPathes[0].RemoveAt(0);
                                }
                                ActiveElements.RemoveAt(i);
                                ActiveElements.Add(unit1);
                                InRange();
                                pictureBox1.Refresh();
                                miniMap.Refresh();
                                break;
                            }
                            else
                            {
                                List<Point> newPath = FindPath(Blocks, new Point(unit1.iX, unit1.iY), ListOfPathes[0].Last(), unit1.BlockIndice);
                                if (newPath != null)
                                {
                                    unit1.realPoint = new Point(unit1.iX * 32, unit1.iY * 32);
                                    newPath.RemoveAt(0);
                                    ListOfPathes[0] = newPath;
                                }
                                else
                                {
                                    unit1.realPoint = new Point(unit1.iX * 32, unit1.iY * 32);
                                    InRange();
                                    Moving_Timer1.Stop();
                                    ListOfMovingCharacters[0] = new Point(-1, -1);
                                }
                            }
                        }else if(unit1.BlockIndice == 9)
                        {
                            if (Blocks[Nextpoint.X, Nextpoint.Y] == 0 /*&& !busy*/)
                            {
                                if (unit1.iX == Nextpoint.X && unit1.iY != Nextpoint.Y)
                                {
                                    if (unit1.iY > Nextpoint.Y)
                                    {
                                        unit1.realPoint = new PointF(unit1.realPoint.X, unit1.realPoint.Y - 6.4f);
                                        side = 1;
                                    }
                                    else
                                    {
                                        unit1.realPoint = new PointF(unit1.realPoint.X, unit1.realPoint.Y + 6.4f);
                                        side = 5;
                                    }
                                }
                                else if (unit1.iY == Nextpoint.Y && unit1.iX != Nextpoint.X)
                                {
                                    if (unit1.iX > Nextpoint.X)
                                    {
                                        unit1.realPoint = new PointF(unit1.realPoint.X - 6.4f, unit1.realPoint.Y);
                                        side = -3;
                                    }
                                    else
                                    {
                                        unit1.realPoint = new PointF(unit1.realPoint.X + 6.4f, unit1.realPoint.Y);
                                        side = 3;
                                    }
                                }
                                else if (unit1.iX != Nextpoint.X && unit1.iY != Nextpoint.Y)
                                {
                                    if (unit1.iX > Nextpoint.X && unit1.iY > Nextpoint.Y)
                                    {
                                        unit1.realPoint = new PointF(unit1.realPoint.X - 6.4f, unit1.realPoint.Y - 6.4f);
                                        side = -2;
                                    }
                                    else if (unit1.iX > Nextpoint.X && unit1.iY < Nextpoint.Y)
                                    {
                                        unit1.realPoint = new PointF(unit1.realPoint.X - 6.4f, unit1.realPoint.Y + 6.4f);
                                        side = -4;
                                    }
                                    else if (unit1.iX < Nextpoint.X && unit1.iY > Nextpoint.Y)
                                    {
                                        unit1.realPoint = new PointF(unit1.realPoint.X + 6.4f, unit1.realPoint.Y - 6.4f);
                                        side = 2;
                                    }
                                    else if (unit1.iX < Nextpoint.X && unit1.iY < Nextpoint.Y)
                                    {
                                        unit1.realPoint = new PointF(unit1.realPoint.X + 6.4f, unit1.realPoint.Y + 6.4f);
                                        side = 4;
                                    }
                                }
                                IndicesOfPathes[0]++;
                                unit1.Frame = GetBMPofFrame(side, IndicesOfPathes[0], unit1.BlockIndice);
                                if (IndicesOfPathes[0] == 5)
                                {
                                    Blocks[unit1.iX, unit1.iY] = 0;
                                    unit1.iX = Nextpoint.X;
                                    unit1.iY = Nextpoint.Y;
                                    unit1.realPoint = new Point(unit1.iX * 32, unit1.iY * 32);
                                    if (unit1.BlockIndice == 9)
                                        Blocks[Nextpoint.X, Nextpoint.Y] = 9;
                                    if (unit1.BlockIndice == 10)
                                        Blocks[Nextpoint.X, Nextpoint.Y] = 10;
                                    ListOfMovingCharacters[0] = Nextpoint;
                                    IndicesOfPathes[0] = 0;
                                    ListOfPathes[0].RemoveAt(0);
                                }
                                ActiveElements.RemoveAt(i);
                                ActiveElements.Add(unit1);
                                InRange();
                                pictureBox1.Refresh();
                                miniMap.Refresh();
                                break;
                            }
                            else
                            {
                                List<Point> newPath = FindPath(Blocks, new Point(unit1.iX, unit1.iY), ListOfPathes[0].Last(), unit1.BlockIndice);
                                if (newPath != null)
                                {
                                    unit1.realPoint = new Point(unit1.iX * 32, unit1.iY * 32);
                                    newPath.RemoveAt(0);
                                    ListOfPathes[0] = newPath;
                                }
                                else
                                {
                                    unit1.realPoint = new Point(unit1.iX * 32, unit1.iY * 32);
                                    InRange();
                                    Moving_Timer1.Stop();
                                    ListOfMovingCharacters[0] = new Point(-1, -1);
                                }
                            }
                        }
                        
                    }
                }
            }
            if (ListOfPathes[0].Count() == 0)
            {
                Moving_Timer1.Stop();
                ListOfMovingCharacters[0] = new Point(-1, -1);
            }
        }

        private void Moving_Timer2_Tick(object sender, EventArgs e)
        {
            if (/*IndicesOfPathes[1] < 5 && */ListOfPathes[1].Count > 0)
            {
                for (int i = 0; i < ActiveElements.Count; ++i)
                {
                    if (ActiveElements[i].iX == ListOfMovingCharacters[1].X && ActiveElements[i].iY == ListOfMovingCharacters[1].Y && ActiveElements[i].Type == "Unit")
                    {
                        int side = 0;
                        Unit unit2 = ActiveElements[i] as Unit;
                        Point Nextpoint = ListOfPathes[1][0];
                        /*bool busy = false;
                        for(int j = 0; j < ActiveElements.Count; ++j)
                        {
                            if (ActiveElements[j].Type == "Unit")
                            {
                                Unit busyUnit = ActiveElements[j] as Unit;
                                if(busyUnit.realPoint.X - 2 <= unit1.realPoint.X && busyUnit.realPoint.X + 2 >= unit1.realPoint.X && busyUnit.realPoint.Y - 2 <= unit1.realPoint.Y && busyUnit.realPoint.Y + 2 >= unit1.realPoint.Y && unit1.iX != busyUnit.iX && unit1.iY != busyUnit.iY)
                                {
                                    busy = true;
                                }
                            }
                        }*/
                        if (unit2.BlockIndice == 10)
                        {
                            if ((Blocks[Nextpoint.X, Nextpoint.Y] == 0 || Blocks[Nextpoint.X, Nextpoint.Y] == 5) /*&& !busy*/)
                            {
                                if (unit2.iX == Nextpoint.X && unit2.iY != Nextpoint.Y)
                                {
                                    if (unit2.iY > Nextpoint.Y)
                                    {
                                        unit2.realPoint = new PointF(unit2.realPoint.X, unit2.realPoint.Y - 6.4f);
                                        side = 1;
                                    }
                                    else
                                    {
                                        unit2.realPoint = new PointF(unit2.realPoint.X, unit2.realPoint.Y + 6.4f);
                                        side = 5;
                                    }
                                }
                                else if (unit2.iY == Nextpoint.Y && unit2.iX != Nextpoint.X)
                                {
                                    if (unit2.iX > Nextpoint.X)
                                    {
                                        unit2.realPoint = new PointF(unit2.realPoint.X - 6.4f, unit2.realPoint.Y);
                                        side = -3;
                                    }
                                    else
                                    {
                                        unit2.realPoint = new PointF(unit2.realPoint.X + 6.4f, unit2.realPoint.Y);
                                        side = 3;
                                    }
                                }
                                else if (unit2.iX != Nextpoint.X && unit2.iY != Nextpoint.Y)
                                {
                                    if (unit2.iX > Nextpoint.X && unit2.iY > Nextpoint.Y)
                                    {
                                        unit2.realPoint = new PointF(unit2.realPoint.X - 6.4f, unit2.realPoint.Y - 6.4f);
                                        side = -2;
                                    }
                                    else if (unit2.iX > Nextpoint.X && unit2.iY < Nextpoint.Y)
                                    {
                                        unit2.realPoint = new PointF(unit2.realPoint.X - 6.4f, unit2.realPoint.Y + 6.4f);
                                        side = -4;
                                    }
                                    else if (unit2.iX < Nextpoint.X && unit2.iY > Nextpoint.Y)
                                    {
                                        unit2.realPoint = new PointF(unit2.realPoint.X + 6.4f, unit2.realPoint.Y - 6.4f);
                                        side = 2;
                                    }
                                    else if (unit2.iX < Nextpoint.X && unit2.iY < Nextpoint.Y)
                                    {
                                        unit2.realPoint = new PointF(unit2.realPoint.X + 6.4f, unit2.realPoint.Y + 6.4f);
                                        side = 4;
                                    }
                                }
                                IndicesOfPathes[1]++;
                                unit2.Frame = GetBMPofFrame(side, IndicesOfPathes[1], unit2.BlockIndice);
                                if (IndicesOfPathes[1] == 5)
                                {
                                    if (Blocks[unit2.iX, unit2.iY] == 11)
                                    {
                                        Blocks[unit2.iX, unit2.iY] = 5;
                                    }
                                    else
                                    {
                                        Blocks[unit2.iX, unit2.iY] = 0;
                                    }
                                    unit2.iX = Nextpoint.X;
                                    unit2.iY = Nextpoint.Y;
                                    unit2.realPoint = new Point(unit2.iX * 32, unit2.iY * 32);
                                    if (unit2.BlockIndice == 9)
                                        Blocks[Nextpoint.X, Nextpoint.Y] = 9;
                                    if (unit2.BlockIndice == 10)
                                    {
                                        if (Blocks[Nextpoint.X, Nextpoint.Y] == 5)
                                            Blocks[Nextpoint.X, Nextpoint.Y] = 11;
                                        else
                                            Blocks[Nextpoint.X, Nextpoint.Y] = 10;
                                    }
                                    ListOfMovingCharacters[1] = Nextpoint;
                                    IndicesOfPathes[1] = 0;
                                    ListOfPathes[1].RemoveAt(0);
                                }
                                ActiveElements.RemoveAt(i);
                                ActiveElements.Add(unit2);
                                InRange();
                                pictureBox1.Refresh();
                                miniMap.Refresh();
                                break;
                            }
                            else
                            {
                                List<Point> newPath = FindPath(Blocks, new Point(unit2.iX, unit2.iY), ListOfPathes[1].Last(), unit2.BlockIndice);
                                if (newPath != null)
                                {
                                    unit2.realPoint = new Point(unit2.iX * 32, unit2.iY * 32);
                                    newPath.RemoveAt(0);
                                    ListOfPathes[1] = newPath;
                                }
                                else
                                {
                                    unit2.realPoint = new Point(unit2.iX * 32, unit2.iY * 32);
                                    InRange();
                                    Moving_Timer2.Stop();
                                    ListOfMovingCharacters[1] = new Point(-1, -1);
                                }
                            }
                        }
                        else if (unit2.BlockIndice == 9)
                        {
                            if (Blocks[Nextpoint.X, Nextpoint.Y] == 0 /*&& !busy*/)
                            {
                                if (unit2.iX == Nextpoint.X && unit2.iY != Nextpoint.Y)
                                {
                                    if (unit2.iY > Nextpoint.Y)
                                    {
                                        unit2.realPoint = new PointF(unit2.realPoint.X, unit2.realPoint.Y - 6.4f);
                                        side = 1;
                                    }
                                    else
                                    {
                                        unit2.realPoint = new PointF(unit2.realPoint.X, unit2.realPoint.Y + 6.4f);
                                        side = 5;
                                    }
                                }
                                else if (unit2.iY == Nextpoint.Y && unit2.iX != Nextpoint.X)
                                {
                                    if (unit2.iX > Nextpoint.X)
                                    {
                                        unit2.realPoint = new PointF(unit2.realPoint.X - 6.4f, unit2.realPoint.Y);
                                        side = -3;
                                    }
                                    else
                                    {
                                        unit2.realPoint = new PointF(unit2.realPoint.X + 6.4f, unit2.realPoint.Y);
                                        side = 3;
                                    }
                                }
                                else if (unit2.iX != Nextpoint.X && unit2.iY != Nextpoint.Y)
                                {
                                    if (unit2.iX > Nextpoint.X && unit2.iY > Nextpoint.Y)
                                    {
                                        unit2.realPoint = new PointF(unit2.realPoint.X - 6.4f, unit2.realPoint.Y - 6.4f);
                                        side = -2;
                                    }
                                    else if (unit2.iX > Nextpoint.X && unit2.iY < Nextpoint.Y)
                                    {
                                        unit2.realPoint = new PointF(unit2.realPoint.X - 6.4f, unit2.realPoint.Y + 6.4f);
                                        side = -4;
                                    }
                                    else if (unit2.iX < Nextpoint.X && unit2.iY > Nextpoint.Y)
                                    {
                                        unit2.realPoint = new PointF(unit2.realPoint.X + 6.4f, unit2.realPoint.Y - 6.4f);
                                        side = 2;
                                    }
                                    else if (unit2.iX < Nextpoint.X && unit2.iY < Nextpoint.Y)
                                    {
                                        unit2.realPoint = new PointF(unit2.realPoint.X + 6.4f, unit2.realPoint.Y + 6.4f);
                                        side = 4;
                                    }
                                }
                                IndicesOfPathes[1]++;
                                unit2.Frame = GetBMPofFrame(side, IndicesOfPathes[1], unit2.BlockIndice);
                                if (IndicesOfPathes[1] == 5)
                                {
                                    Blocks[unit2.iX, unit2.iY] = 0;
                                    unit2.iX = Nextpoint.X;
                                    unit2.iY = Nextpoint.Y;
                                    unit2.realPoint = new Point(unit2.iX * 32, unit2.iY * 32);
                                    if (unit2.BlockIndice == 9)
                                        Blocks[Nextpoint.X, Nextpoint.Y] = 9;
                                    if (unit2.BlockIndice == 10)
                                        Blocks[Nextpoint.X, Nextpoint.Y] = 10;
                                    ListOfMovingCharacters[1] = Nextpoint;
                                    IndicesOfPathes[1] = 0;
                                    ListOfPathes[1].RemoveAt(0);
                                }
                                ActiveElements.RemoveAt(i);
                                ActiveElements.Add(unit2);
                                InRange();
                                pictureBox1.Refresh();
                                miniMap.Refresh();
                                break;
                            }
                            else
                            {
                                List<Point> newPath = FindPath(Blocks, new Point(unit2.iX, unit2.iY), ListOfPathes[1].Last(), unit2.BlockIndice);
                                if (newPath != null)
                                {
                                    unit2.realPoint = new Point(unit2.iX * 32, unit2.iY * 32);
                                    newPath.RemoveAt(0);
                                    ListOfPathes[1] = newPath;
                                }
                                else
                                {
                                    unit2.realPoint = new Point(unit2.iX * 32, unit2.iY * 32);
                                    InRange();
                                    Moving_Timer2.Stop();
                                    ListOfMovingCharacters[1] = new Point(-1, -1);
                                }
                            }
                        }

                    }
                }
            }
            if (ListOfPathes[1].Count() == 0)
            {
                Moving_Timer2.Stop();
                ListOfMovingCharacters[1] = new Point(-1, -1);
            }
        }

        private void Moving_Timer3_Tick(object sender, EventArgs e) 
        {
            if (IndicesOfPathes[2] < 5 && ListOfPathes[2].Count > 0)
            {
                for (int i = 0; i < ActiveElements.Count; ++i)
                {
                    if (ActiveElements[i].iX == ListOfMovingCharacters[2].X && ActiveElements[i].iY == ListOfMovingCharacters[2].Y && ActiveElements[i].Type == "Unit")
                    {
                        int side = 0;
                        Unit unit3 = ActiveElements[i] as Unit;
                        Point Nextpoint = ListOfPathes[2][0];
                        /*bool busy = false;
                        for (int j = 0; j < ActiveElements.Count; ++j)
                        {
                            if (ActiveElements[j].Type == "Unit")
                            {
                                Unit busyUnit = ActiveElements[j] as Unit;
                                if (busyUnit.realPoint.X - 2 <= unit3.realPoint.X && busyUnit.realPoint.X + 2 >= unit3.realPoint.X && busyUnit.realPoint.Y - 2 <= unit3.realPoint.Y && busyUnit.realPoint.Y + 2 >= unit3.realPoint.Y && unit3.iX != busyUnit.iX && unit3.iY != busyUnit.iY)
                                {
                                    busy = true;
                                }
                            }
                        }*/
                        if(unit3.BlockIndice == 10)
                        {
                            if (Blocks[Nextpoint.X, Nextpoint.Y] == 0 || Blocks[Nextpoint.X, Nextpoint.Y] == 1 /*&& !busy*/)
                            {
                                if (unit3.iX == Nextpoint.X && unit3.iY != Nextpoint.Y)
                                {
                                    if (unit3.iY > Nextpoint.Y)
                                    {
                                        unit3.realPoint = new PointF(unit3.realPoint.X, unit3.realPoint.Y - 6.4f);
                                        side = 1;
                                    }
                                    else
                                    {
                                        unit3.realPoint = new PointF(unit3.realPoint.X, unit3.realPoint.Y + 6.4f);
                                        side = 5;
                                    }
                                }
                                else if (unit3.iY == Nextpoint.Y && unit3.iX != Nextpoint.X)
                                {
                                    if (unit3.iX > Nextpoint.X)
                                    {
                                        unit3.realPoint = new PointF(unit3.realPoint.X - 6.4f, unit3.realPoint.Y);
                                        side = -3;
                                    }
                                    else
                                    {
                                        unit3.realPoint = new PointF(unit3.realPoint.X + 6.4f, unit3.realPoint.Y);
                                        side = 3;
                                    }
                                }
                                else if (unit3.iX != Nextpoint.X && unit3.iY != Nextpoint.Y)
                                {
                                    if (unit3.iX > Nextpoint.X && unit3.iY > Nextpoint.Y)
                                    {
                                        unit3.realPoint = new PointF(unit3.realPoint.X - 6.4f, unit3.realPoint.Y - 6.4f);
                                        side = -2;
                                    }
                                    else if (unit3.iX > Nextpoint.X && unit3.iY < Nextpoint.Y)
                                    {
                                        unit3.realPoint = new PointF(unit3.realPoint.X - 6.4f, unit3.realPoint.Y + 6.4f);
                                        side = -4;
                                    }
                                    else if (unit3.iX < Nextpoint.X && unit3.iY > Nextpoint.Y)
                                    {
                                        unit3.realPoint = new PointF(unit3.realPoint.X + 6.4f, unit3.realPoint.Y - 6.4f);
                                        side = 2;
                                    }
                                    else if (unit3.iX < Nextpoint.X && unit3.iY < Nextpoint.Y)
                                    {
                                        unit3.realPoint = new PointF(unit3.realPoint.X + 6.4f, unit3.realPoint.Y + 6.4f);
                                        side = 4;
                                    }
                                }
                                IndicesOfPathes[2]++;
                                unit3.Frame = GetBMPofFrame(side, IndicesOfPathes[2], unit3.BlockIndice);
                                if (IndicesOfPathes[2] == 5)
                                {
                                    if (Blocks[unit3.iX, unit3.iY] == 11)
                                    {
                                        Blocks[unit3.iX, unit3.iY] = 5;
                                    }
                                    else
                                    {
                                        Blocks[unit3.iX, unit3.iY] = 0;
                                    }
                                    unit3.iX = Nextpoint.X;
                                    unit3.iY = Nextpoint.Y;
                                    unit3.realPoint = new Point(unit3.iX * 32, unit3.iY * 32);
                                    if (unit3.BlockIndice == 9)
                                        Blocks[Nextpoint.X, Nextpoint.Y] = 9;
                                    if (unit3.BlockIndice == 10)
                                    {
                                        if (Blocks[Nextpoint.X, Nextpoint.Y] == 5)
                                            Blocks[Nextpoint.X, Nextpoint.Y] = 11;
                                        else
                                            Blocks[Nextpoint.X, Nextpoint.Y] = 10;
                                    }
                                    ListOfMovingCharacters[2] = Nextpoint;
                                    IndicesOfPathes[2] = 0;
                                    ListOfPathes[2].RemoveAt(0);
                                }
                                ActiveElements.RemoveAt(i);
                                ActiveElements.Add(unit3);
                                InRange();
                                pictureBox1.Refresh();
                                miniMap.Refresh();
                                break;
                            }
                            else
                            {
                                List<Point> newPath = FindPath(Blocks, new Point(unit3.iX, unit3.iY), ListOfPathes[2].Last(), unit3.BlockIndice);
                                if (newPath != null)
                                {
                                    unit3.realPoint = new Point(unit3.iX, unit3.iY);
                                    newPath.RemoveAt(0);
                                    ListOfPathes[2] = newPath;
                                }
                                else
                                {
                                    unit3.realPoint = new Point(unit3.iX * 32, unit3.iY * 32);
                                    InRange();
                                    Moving_Timer3.Stop();
                                    ListOfMovingCharacters[2] = new Point(-1, -1);
                                }
                            }
                        }else if(unit3.BlockIndice == 9)
                        {
                            if (Blocks[Nextpoint.X, Nextpoint.Y] == 0 /*&& !busy*/)
                            {
                                if (unit3.iX == Nextpoint.X && unit3.iY != Nextpoint.Y)
                                {
                                    if (unit3.iY > Nextpoint.Y)
                                    {
                                        unit3.realPoint = new PointF(unit3.realPoint.X, unit3.realPoint.Y - 6.4f);
                                        side = 1;
                                    }
                                    else
                                    {
                                        unit3.realPoint = new PointF(unit3.realPoint.X, unit3.realPoint.Y + 6.4f);
                                        side = 5;
                                    }
                                }
                                else if (unit3.iY == Nextpoint.Y && unit3.iX != Nextpoint.X)
                                {
                                    if (unit3.iX > Nextpoint.X)
                                    {
                                        unit3.realPoint = new PointF(unit3.realPoint.X - 6.4f, unit3.realPoint.Y);
                                        side = -3;
                                    }
                                    else
                                    {
                                        unit3.realPoint = new PointF(unit3.realPoint.X + 6.4f, unit3.realPoint.Y);
                                        side = 3;
                                    }
                                }
                                else if (unit3.iX != Nextpoint.X && unit3.iY != Nextpoint.Y)
                                {
                                    if (unit3.iX > Nextpoint.X && unit3.iY > Nextpoint.Y)
                                    {
                                        unit3.realPoint = new PointF(unit3.realPoint.X - 6.4f, unit3.realPoint.Y - 6.4f);
                                        side = -2;
                                    }
                                    else if (unit3.iX > Nextpoint.X && unit3.iY < Nextpoint.Y)
                                    {
                                        unit3.realPoint = new PointF(unit3.realPoint.X - 6.4f, unit3.realPoint.Y + 6.4f);
                                        side = -4;
                                    }
                                    else if (unit3.iX < Nextpoint.X && unit3.iY > Nextpoint.Y)
                                    {
                                        unit3.realPoint = new PointF(unit3.realPoint.X + 6.4f, unit3.realPoint.Y - 6.4f);
                                        side = 2;
                                    }
                                    else if (unit3.iX < Nextpoint.X && unit3.iY < Nextpoint.Y)
                                    {
                                        unit3.realPoint = new PointF(unit3.realPoint.X + 6.4f, unit3.realPoint.Y + 6.4f);
                                        side = 4;
                                    }
                                }
                                IndicesOfPathes[2]++;
                                unit3.Frame = GetBMPofFrame(side, IndicesOfPathes[2], unit3.BlockIndice);
                                if (IndicesOfPathes[2] == 5)
                                {
                                    Blocks[unit3.iX, unit3.iY] = 0;
                                    unit3.iX = Nextpoint.X;
                                    unit3.iY = Nextpoint.Y;
                                    ListOfMovingCharacters[2] = Nextpoint;
                                    if (unit3.BlockIndice == 9)
                                        Blocks[unit3.iX, unit3.iY] = 9;
                                    if (unit3.BlockIndice == 10)
                                        Blocks[unit3.iX, unit3.iY] = 10;
                                    ListOfMovingCharacters[2] = Nextpoint;
                                    IndicesOfPathes[2] = 0;
                                    ListOfPathes[2].RemoveAt(0);
                                }
                                ActiveElements.RemoveAt(i);
                                ActiveElements.Add(unit3);
                                InRange();
                                pictureBox1.Refresh();
                                miniMap.Refresh();
                                break;
                            }
                            else
                            {
                                List<Point> newPath = FindPath(Blocks, new Point(unit3.iX, unit3.iY), ListOfPathes[2].Last(), unit3.BlockIndice);
                                if (newPath != null)
                                {
                                    unit3.realPoint = new Point(unit3.iX, unit3.iY);
                                    newPath.RemoveAt(0);
                                    ListOfPathes[2] = newPath;
                                }
                                else
                                {
                                    unit3.realPoint = new Point(unit3.iX * 32, unit3.iY * 32);
                                    InRange();
                                    Moving_Timer3.Stop();
                                    ListOfMovingCharacters[2] = new Point(-1, -1);
                                }
                            }
                        }
                        
                    }
                }
            }
            if (ListOfPathes[2].Count() == 0)
            {
                Moving_Timer3.Stop();
                ListOfMovingCharacters[2] = new Point(-1, -1);
            }
        }

        private void GlobalTimer_Tick(object sender, EventArgs e)
        {
            actualTime.seconds ++;
            if(actualTime.seconds > 60)
            {
                actualTime.seconds -= 60;
                actualTime.minutes++;
            }
            if (actualTime.minutes < 10 || actualTime.seconds < 10)
            {
                if (actualTime.minutes < 10 && actualTime.seconds < 10)
                {
                    ActualTime.Text = "0" + actualTime.minutes.ToString() + " : 0" + actualTime.seconds.ToString();
                }
                else if (actualTime.minutes < 10)
                {
                    ActualTime.Text = "0" + actualTime.minutes.ToString() + " : " + actualTime.seconds.ToString();
                }
                else
                {
                    ActualTime.Text = actualTime.minutes.ToString() + " : 0" + actualTime.seconds.ToString();
                }
            }
            else
            {
                ActualTime.Text = actualTime.minutes.ToString() + " : " + actualTime.seconds.ToString();
            }
            for (int i = 0; i < AllBuildings.Count; i++)
            {
                if (AllBuildings[i].Type == "Building")
                {
                    Building building = AllBuildings[i] as Building;
                    if(building.Resource == "Gold")
                    {
                        NowGold += building.ResourceValue;
                        GoldAmount.Text = NowGold.ToString();
                    }
                    if(building.Resource == "Food")
                    {
                        NowFood += building.ResourceValue;
                        FoodAmount.Text = NowFood.ToString();
                    }
                    if(building.Resource == "Stone")
                    {
                        NowStone += building.ResourceValue;
                        StoneAmount.Text = NowStone.ToString();
                    }
                }
            }
            NowWood += 1;
            WoodAmount.Text = NowWood.ToString();
        }

        private void Extraction_Click(object sender, EventArgs e)
        {
            Moving = false;
            Creation = false;
            Delete = false;
            Extract = true;
            HideAllIcons();
            CancelAction.Visible = true;
            pictureBox1.Refresh();
        }

        private void Extraction_MouseEnter(object sender, EventArgs e)
        {
            pictureBox7.Image = null;
            pictureBox8.Image = null;
            NessRes1.Text = "";
            NessRes2.Text = "";
            NameOfButton.Text = "The conquest of resources";
            NecessaryResources.Visible = true;
        }

        private void Extraction_MouseLeave(object sender, EventArgs e)
        {
            NecessaryResources.Visible = false;
        }

        private void StopUnit_Click(object sender, EventArgs e)
        {
            StoppedUnit(SelectedUnit);
        }

        private void Attack_timer_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < ActiveElements.Count; ++i)
            {
                if (ActiveElements[i].Type == "Unit")
                {
                    Unit unit = ActiveElements[i] as Unit;
                    if (unit.target != null)
                    {
                        StoppedUnit(unit);
                        unit.AttackIndice++;
                        int side = FindSide(new Point(unit.iX, unit.iY), new Point(unit.target.iX, unit.target.iY));
                        unit.Frame = GetBMPforAttack(side, unit.AttackIndice, unit.BlockIndice);
                        if(unit.AttackIndice == 2)
                        {
                            unit.AttackIndice = 0;
                            unit.target.Hp -= unit.AttackDamage;
                            if(unit.target.Hp < 0)
                            {
                                unit.target.Frame = GetBMPforCorpse(unit.target.BlockIndice);
                                unit.target.target = null;
                                Enemies.Remove(unit.target);
                                Blocks[unit.target.iX, unit.target.iY] = 0;
                                unit.target = null;
                                InRange();
                            }
                        }
                    }
                }
            }
            for (int i = 0; i < Enemies.Count; ++i)
            {
                if (Enemies[i].buildingtarget == null)
                {
                    if (Enemies[i].target != null)
                    {
                        StoppedUnit(Enemies[i]);
                        Enemies[i].AttackIndice++;
                        int side = FindSide(new Point(Enemies[i].iX, Enemies[i].iY), new Point(Enemies[i].target.iX, Enemies[i].target.iY));
                        Enemies[i].Frame = GetBMPforAttack(side, Enemies[i].AttackIndice, Enemies[i].BlockIndice);
                        if (Enemies[i].AttackIndice == 2)
                        {
                            Enemies[i].AttackIndice = 0;
                            Enemies[i].target.Hp -= Enemies[i].AttackDamage;
                            if (Enemies[i].target.Hp < 0)
                            {
                                Enemies[i].target.Frame = GetBMPforCorpse(Enemies[i].target.BlockIndice);
                                Enemies[i].target.target = null;
                                ActiveElements.Remove(Enemies[i].target);
                                Blocks[Enemies[i].target.iX, Enemies[i].target.iY] = 0;
                                Enemies[i].target = null;
                                InRange();
                                if (Enemies[i].target == null)
                                {
                                    Point TH = new Point(4, 4);
                                    for (int j = 0; j < ActiveElements.Count; ++j)
                                    {
                                        if (ActiveElements[j].BlockIndice == 6)
                                        {
                                            TH = new Point(ActiveElements[j].iX, ActiveElements[j].iY);
                                        }
                                    }
                                    List<Point> Path = FindPath(Blocks, new Point(Enemies[i].iX, Enemies[i].iY), TH, Enemies[i].BlockIndice);
                                    if (Path != null)
                                    {
                                        Path.RemoveAt(0);
                                        if (!Enemy_moving1.Enabled)
                                        {
                                            ListOfEnemyPathes[0] = Path;
                                            ListOfMovingEnemies[0] = new Point(Enemies[i].iX, Enemies[i].iY);
                                            IndicesOfEnemyPathes[0] = 0;
                                            Enemy_moving1.Start();
                                        }
                                        else if (!Enemy_moving2.Enabled)
                                        {
                                            ListOfEnemyPathes[1] = Path;
                                            ListOfMovingEnemies[1] = new Point(Enemies[i].iX, Enemies[i].iY);
                                            IndicesOfEnemyPathes[1] = 0;
                                            Enemy_moving2.Start();
                                        }
                                        else if (!Enemy_moving3.Enabled)
                                        {
                                            ListOfEnemyPathes[2] = Path;
                                            ListOfMovingEnemies[2] = new Point(Enemies[i].iX, Enemies[i].iY);
                                            IndicesOfEnemyPathes[2] = 0;
                                            Enemy_moving3.Start();
                                        }
                                        else if (!Enemy_moving4.Enabled)
                                        {
                                            ListOfEnemyPathes[3] = Path;
                                            ListOfMovingEnemies[3] = new Point(Enemies[i].iX, Enemies[i].iY);
                                            IndicesOfEnemyPathes[3] = 0;
                                            Enemy_moving4.Start();
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        Point TH = new Point(4, 4);
                        for (int j = 0; j < ActiveElements.Count; ++j)
                        {
                            if (ActiveElements[j].BlockIndice == 6)
                            {
                                TH = new Point(ActiveElements[j].iX + 1, ActiveElements[j].iY + 1);
                            }
                        }
                        List<Point> Path = FindPath(Blocks, new Point(Enemies[i].iX, Enemies[i].iY), TH, Enemies[i].BlockIndice);
                        if (Path != null)
                        {
                            Path.RemoveAt(0);
                            if (!Enemy_moving1.Enabled)
                            {
                                ListOfEnemyPathes[0] = Path;
                                ListOfMovingEnemies[0] = new Point(Enemies[i].iX, Enemies[i].iY);
                                IndicesOfEnemyPathes[0] = 0;
                                Enemy_moving1.Start();
                            }
                            else if (!Enemy_moving2.Enabled)
                            {
                                ListOfEnemyPathes[1] = Path;
                                ListOfMovingEnemies[1] = new Point(Enemies[i].iX, Enemies[i].iY);
                                IndicesOfEnemyPathes[1] = 0;
                                Enemy_moving2.Start();
                            }
                            else if (!Enemy_moving3.Enabled)
                            {
                                ListOfEnemyPathes[2] = Path;
                                ListOfMovingEnemies[2] = new Point(Enemies[i].iX, Enemies[i].iY);
                                IndicesOfEnemyPathes[2] = 0;
                                Enemy_moving3.Start();
                            }
                            else if (!Enemy_moving4.Enabled)
                            {
                                ListOfEnemyPathes[3] = Path;
                                ListOfMovingEnemies[3] = new Point(Enemies[i].iX, Enemies[i].iY);
                                IndicesOfEnemyPathes[3] = 0;
                                Enemy_moving4.Start();
                            }
                        }
                    }
                }
                else
                {
                    StoppedUnit(Enemies[i]);
                    Enemies[i].AttackIndice++;
                    if(Enemies[i].buildingtarget != null)
                    {
                        int side = FindSide(new Point(Enemies[i].iX, Enemies[i].iY), new Point(Enemies[i].buildingtarget.iX, Enemies[i].buildingtarget.iY));
                        Enemies[i].Frame = GetBMPforAttack(side, Enemies[i].AttackIndice, Enemies[i].BlockIndice);
                        if (Enemies[i].AttackIndice == 2)
                        {
                            Enemies[i].AttackIndice = 0;
                            Enemies[i].buildingtarget.Hp -= Enemies[i].AttackDamage;
                            if (Enemies[i].buildingtarget.Hp < 0)
                            {
                                if (Enemies[i].buildingtarget.BlockIndice == 6)
                                {
                                    System.Windows.Forms.Application.Exit();
                                }
                                ActiveElements.Remove(Enemies[i].buildingtarget);
                                AllBuildings.Remove(Enemies[i].buildingtarget);
                                for (int x = Enemies[i].buildingtarget.iX; x < Enemies[i].buildingtarget.iX + Enemies[i].buildingtarget.Size; ++x)
                                {
                                    for (int y = Enemies[i].buildingtarget.iY; y < Enemies[i].buildingtarget.iY + Enemies[i].buildingtarget.Size; ++y)
                                    {
                                        using (Graphics g = Graphics.FromImage(MapBitmap))
                                        {
                                            Object r = Properties.Resources.ResourceManager.GetObject("_01");
                                            Bitmap grass = (Bitmap)r;
                                            g.DrawImage(grass, x * 32, y * 32);
                                        }
                                        Blocks[x, y] = 0;
                                    }
                                }
                                Enemies[i].buildingtarget = null;
                                InRange();
                                if (Enemies[i].target == null)
                                {
                                    Point TH = new Point(4, 4);
                                    for (int j = 0; j < ActiveElements.Count; ++j)
                                    {
                                        if (ActiveElements[j].BlockIndice == 6)
                                        {
                                            TH = new Point(ActiveElements[j].iX + 1, ActiveElements[j].iY + 1);
                                        }
                                    }
                                    List<Point> Path = FindPath(Blocks, new Point(Enemies[i].iX, Enemies[i].iY), TH, Enemies[i].BlockIndice);
                                    if (Path != null)
                                    {
                                        Path.RemoveAt(0);
                                        if (!Enemy_moving1.Enabled)
                                        {
                                            ListOfEnemyPathes[0] = Path;
                                            ListOfMovingEnemies[0] = new Point(Enemies[i].iX, Enemies[i].iY);
                                            IndicesOfEnemyPathes[0] = 0;
                                            Enemy_moving1.Start();
                                        }
                                        else if (!Enemy_moving2.Enabled)
                                        {
                                            ListOfEnemyPathes[1] = Path;
                                            ListOfMovingEnemies[1] = new Point(Enemies[i].iX, Enemies[i].iY);
                                            IndicesOfEnemyPathes[1] = 0;
                                            Enemy_moving2.Start();
                                        }
                                        else if (!Enemy_moving3.Enabled)
                                        {
                                            ListOfEnemyPathes[2] = Path;
                                            ListOfMovingEnemies[2] = new Point(Enemies[i].iX, Enemies[i].iY);
                                            IndicesOfEnemyPathes[2] = 0;
                                            Enemy_moving3.Start();
                                        }
                                        else if (!Enemy_moving4.Enabled)
                                        {
                                            ListOfEnemyPathes[3] = Path;
                                            ListOfMovingEnemies[3] = new Point(Enemies[i].iX, Enemies[i].iY);
                                            IndicesOfEnemyPathes[3] = 0;
                                            Enemy_moving4.Start();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if(SpawnIndice >= 1 && Enemies.Count == 0)
            {
                System.Windows.Forms.Application.Exit();
            }
            pictureBox1.Refresh();
            miniMap.Refresh();
        }

        private void Spawn_Enemies_Tick(object sender, EventArgs e)
        {
            SpawnIndice++;
            if(SpawnIndice == 1)
            {
                Unit Ogre1 = new Unit(3, 2, 12, 1, 1, 45, 45, "Unit", 3, new PointF(3 * 32, 2 * 32), 1, null, null);
                Point TH = new Point(4, 4);
                for(int i = 0; i < ActiveElements.Count; ++i)
                {
                    if (ActiveElements[i].BlockIndice == 6)
                    {
                        TH = new Point(ActiveElements[i].iX + 1, ActiveElements[i].iY + 1);
                    }
                }
                int side = FindSide(new Point(Ogre1.iX, Ogre1.iY), TH);
                Ogre1.Frame = GetBMPofFrame(side, 5, Ogre1.BlockIndice);
                Enemies.Add(Ogre1);
                Blocks[3, 2] = 12;

                Unit Ogre2 = new Unit(2, 3, 12, 1, 1, 45, 45, "Unit", 3, new PointF(2 * 32, 3 * 32), 1, null, null);
                for (int i = 0; i < ActiveElements.Count; ++i)
                {
                    if (ActiveElements[i].BlockIndice == 6)
                    {
                        TH = new Point(ActiveElements[i].iX, ActiveElements[i].iY);
                    }
                }
                side = FindSide(new Point(Ogre2.iX, Ogre2.iY), TH);
                Ogre2.Frame = GetBMPofFrame(side, 5, Ogre2.BlockIndice);
                Enemies.Add(Ogre2);
                Blocks[2, 3] = 12;
                for (int i = 0; i < Enemies.Count; ++i)
                {
                    List<Point> Path = FindPath(Blocks, new Point(Enemies[i].iX, Enemies[i].iY), TH, Enemies[i].BlockIndice);
                    if (Path != null)
                    {
                        Path.RemoveAt(0);
                        if (!Enemy_moving1.Enabled)
                        {
                            ListOfEnemyPathes[0] = Path;
                            ListOfMovingEnemies[0] = new Point(Enemies[i].iX, Enemies[i].iY);
                            IndicesOfEnemyPathes[0] = 0;
                            Enemy_moving1.Start();
                        }
                        else if (!Enemy_moving2.Enabled)
                        {
                            ListOfEnemyPathes[1] = Path;
                            ListOfMovingEnemies[1] = new Point(Enemies[i].iX, Enemies[i].iY);
                            IndicesOfEnemyPathes[1] = 0;
                            Enemy_moving2.Start();
                        }
                        else if (!Enemy_moving3.Enabled)
                        {
                            ListOfEnemyPathes[2] = Path;
                            ListOfMovingEnemies[2] = new Point(Enemies[i].iX, Enemies[i].iY);
                            IndicesOfEnemyPathes[2] = 0;
                            Enemy_moving3.Start();
                        }
                        else if (!Enemy_moving4.Enabled)
                        {
                            ListOfEnemyPathes[3] = Path;
                            ListOfMovingEnemies[3] = new Point(Enemies[i].iX, Enemies[i].iY);
                            IndicesOfEnemyPathes[3] = 0;
                            Enemy_moving4.Start();
                        }
                    }
                }
                InRange();
                pictureBox1.Refresh();
                miniMap.Refresh();
            }
            /*else if(SpawnIndice == 2)
            {
                Unit Ogre1 = new Unit(3, 2, 12, 1, 1, 45, 45, "Unit", 3, new PointF(3 * 32, 2 * 32), 1, null, null);
                Point TH = new Point(4, 4);
                for (int i = 0; i < ActiveElements.Count; ++i)
                {
                    if (ActiveElements[i].BlockIndice == 6)
                    {
                        TH = new Point(ActiveElements[i].iX + 1, ActiveElements[i].iY + 1);
                    }
                }
                int side = FindSide(new Point(Ogre1.iX, Ogre1.iY), TH);
                Ogre1.Frame = GetBMPofFrame(side, 5, Ogre1.BlockIndice);
                Enemies.Add(Ogre1);
                Blocks[3, 2] = 12;

                Unit Ogre2 = new Unit(59, 60, 12, 1, 1, 45, 45, "Unit", 3, new PointF(59 * 32, 60 * 32), 1, null, null);
                for (int i = 0; i < ActiveElements.Count; ++i)
                {
                    if (ActiveElements[i].BlockIndice == 6)
                    {
                        TH = new Point(ActiveElements[i].iX, ActiveElements[i].iY);
                    }
                }
                side = FindSide(new Point(Ogre2.iX, Ogre2.iY), TH);
                Ogre2.Frame = GetBMPofFrame(side, 5, Ogre2.BlockIndice);
                Enemies.Add(Ogre2);
                Blocks[59, 60] = 12;

                Unit Catapulte1 = new Unit(60, 59, 13, 1, 1, 60, 60, "Unit", 7, new PointF(60 * 32, 59 * 32), 5, null, null);
                for (int i = 0; i < ActiveElements.Count; ++i)
                {
                    if (ActiveElements[i].BlockIndice == 6)
                    {
                        TH = new Point(ActiveElements[i].iX + 1, ActiveElements[i].iY + 1);
                    }
                }
                side = FindSide(new Point(Catapulte1.iX, Catapulte1.iY), TH);
                Catapulte1.Frame = GetBMPofFrame(side, 5, Catapulte1.BlockIndice);
                Enemies.Add(Catapulte1);
                Blocks[60, 59] = 13;

                Unit Catapulte2 = new Unit(2, 3, 13, 1, 1, 60, 60, "Unit", 7, new PointF(2 * 32, 3 * 32), 5, null, null);
                for (int i = 0; i < ActiveElements.Count; ++i)
                {
                    if (ActiveElements[i].BlockIndice == 6)
                    {
                        TH = new Point(ActiveElements[i].iX, ActiveElements[i].iY);
                    }
                }
                side = FindSide(new Point(Catapulte2.iX, Catapulte2.iY), TH);
                Catapulte2.Frame = GetBMPofFrame(side, 5, Catapulte2.BlockIndice);
                Enemies.Add(Catapulte2);
                Blocks[2, 3] = 13;
                for (int i = 0; i < Enemies.Count; ++i)
                {
                    List<Point> Path = FindPath(Blocks, new Point(Enemies[i].iX, Enemies[i].iY), TH, Enemies[i].BlockIndice);
                    if (Path != null)
                    {
                        Path.RemoveAt(0);
                        if (!Enemy_moving1.Enabled)
                        {
                            ListOfEnemyPathes[0] = Path;
                            ListOfMovingEnemies[0] = new Point(Enemies[i].iX, Enemies[i].iY);
                            IndicesOfEnemyPathes[0] = 0;
                            Enemy_moving1.Start();
                        }
                        else if (!Enemy_moving2.Enabled)
                        {
                            ListOfEnemyPathes[1] = Path;
                            ListOfMovingEnemies[1] = new Point(Enemies[i].iX, Enemies[i].iY);
                            IndicesOfEnemyPathes[1] = 0;
                            Enemy_moving2.Start();
                        }
                        else if (!Enemy_moving3.Enabled)
                        {
                            ListOfEnemyPathes[2] = Path;
                            ListOfMovingEnemies[2] = new Point(Enemies[i].iX, Enemies[i].iY);
                            IndicesOfEnemyPathes[2] = 0;
                            Enemy_moving3.Start();
                        }
                        else if (!Enemy_moving4.Enabled)
                        {
                            ListOfEnemyPathes[3] = Path;
                            ListOfMovingEnemies[3] = new Point(Enemies[i].iX, Enemies[i].iY);
                            IndicesOfEnemyPathes[3] = 0;
                            Enemy_moving4.Start();
                        }
                    }
                }
                InRange();
                pictureBox1.Refresh();
                miniMap.Refresh();
            }*/
        }

        private void Enemy_moving1_Tick(object sender, EventArgs e)
        {
            if (ListOfEnemyPathes[0].Count > 0)
            {
                for (int i = 0; i < Enemies.Count; ++i)
                {
                    if (Enemies[i].iX == ListOfMovingEnemies[0].X && Enemies[i].iY == ListOfMovingEnemies[0].Y)
                    {
                        int side = 0;
                        Unit unit1 = Enemies[i];
                        Point Nextpoint = ListOfEnemyPathes[0][0];
                        /*bool busy = false;
                        for(int j = 0; j < ActiveElements.Count; ++j)
                        {
                            if (ActiveElements[j].Type == "Unit")
                            {
                                Unit busyUnit = ActiveElements[j] as Unit;
                                if(busyUnit.realPoint.X - 2 <= unit1.realPoint.X && busyUnit.realPoint.X + 2 >= unit1.realPoint.X && busyUnit.realPoint.Y - 2 <= unit1.realPoint.Y && busyUnit.realPoint.Y + 2 >= unit1.realPoint.Y && unit1.iX != busyUnit.iX && unit1.iY != busyUnit.iY)
                                {
                                    busy = true;
                                }
                            }
                        }*/
                        if (Blocks[Nextpoint.X, Nextpoint.Y] == 0 /*&& !busy*/)
                        {
                            if (unit1.iX == Nextpoint.X && unit1.iY != Nextpoint.Y)
                            {
                                if (unit1.iY > Nextpoint.Y)
                                {
                                    unit1.realPoint = new PointF(unit1.realPoint.X, unit1.realPoint.Y - 6.4f);
                                    side = 1;
                                }
                                else
                                {
                                    unit1.realPoint = new PointF(unit1.realPoint.X, unit1.realPoint.Y + 6.4f);
                                    side = 5;
                                }
                            }
                            else if (unit1.iY == Nextpoint.Y && unit1.iX != Nextpoint.X)
                            {
                                if (unit1.iX > Nextpoint.X)
                                {
                                    unit1.realPoint = new PointF(unit1.realPoint.X - 6.4f, unit1.realPoint.Y);
                                    side = -3;
                                }
                                else
                                {
                                    unit1.realPoint = new PointF(unit1.realPoint.X + 6.4f, unit1.realPoint.Y);
                                    side = 3;
                                }
                            }
                            else if (unit1.iX != Nextpoint.X && unit1.iY != Nextpoint.Y)
                            {
                                if (unit1.iX > Nextpoint.X && unit1.iY > Nextpoint.Y)
                                {
                                    unit1.realPoint = new PointF(unit1.realPoint.X - 6.4f, unit1.realPoint.Y - 6.4f);
                                    side = -2;
                                }
                                else if (unit1.iX > Nextpoint.X && unit1.iY < Nextpoint.Y)
                                {
                                    unit1.realPoint = new PointF(unit1.realPoint.X - 6.4f, unit1.realPoint.Y + 6.4f);
                                    side = -4;
                                }
                                else if (unit1.iX < Nextpoint.X && unit1.iY > Nextpoint.Y)
                                {
                                    unit1.realPoint = new PointF(unit1.realPoint.X + 6.4f, unit1.realPoint.Y - 6.4f);
                                    side = 2;
                                }
                                else if (unit1.iX < Nextpoint.X && unit1.iY < Nextpoint.Y)
                                {
                                    unit1.realPoint = new PointF(unit1.realPoint.X + 6.4f, unit1.realPoint.Y + 6.4f);
                                    side = 4;
                                }
                            }
                            IndicesOfEnemyPathes[0]++;
                            unit1.Frame = GetBMPofFrame(side, IndicesOfEnemyPathes[0], unit1.BlockIndice);
                            if (IndicesOfEnemyPathes[0] == 5)
                            {
                                Blocks[unit1.iX, unit1.iY] = 0;
                                unit1.iX = Nextpoint.X;
                                unit1.iY = Nextpoint.Y;
                                unit1.realPoint = new Point(unit1.iX * 32, unit1.iY * 32);
                                if (unit1.BlockIndice == 12)
                                    Blocks[Nextpoint.X, Nextpoint.Y] = 12;
                                if (unit1.BlockIndice == 13)
                                    Blocks[Nextpoint.X, Nextpoint.Y] = 13;
                                ListOfMovingEnemies[0] = Nextpoint;
                                IndicesOfEnemyPathes[0] = 0;
                                ListOfEnemyPathes[0].RemoveAt(0);
                            }
                            Enemies.RemoveAt(i);
                            Enemies.Add(unit1);
                            InRange();
                            pictureBox1.Refresh();
                            miniMap.Refresh();
                            break;
                        }else if (Blocks[Nextpoint.X, Nextpoint.Y] == 0 || Blocks[Nextpoint.X, Nextpoint.Y] == 5 || Blocks[Nextpoint.X, Nextpoint.Y] == 2 || Blocks[Nextpoint.X, Nextpoint.Y] == 6)
                        {
                            for(int j = 0; j < AllBuildings.Count; ++j)
                            {
                                if (Nextpoint.X >= AllBuildings[j].iX && Nextpoint.X <= AllBuildings[j].iX + AllBuildings[j].Size - 1 && Nextpoint.Y >= AllBuildings[j].iY && Nextpoint.Y <= AllBuildings[j].iY + AllBuildings[j].Size - 1)
                                {
                                    Enemies[i].buildingtarget = AllBuildings[j];
                                }
                            }
                            StoppedUnit(Enemies[i]);
                        }
                        else
                        {
                            List<Point> newPath = FindPath(Blocks, new Point(unit1.iX, unit1.iY), ListOfEnemyPathes[0].Last(), unit1.BlockIndice);
                            if (newPath != null)
                            {
                                unit1.realPoint = new Point(unit1.iX * 32, unit1.iY * 32);
                                newPath.RemoveAt(0);
                                ListOfEnemyPathes[0] = newPath;
                            }
                            else
                            {
                                unit1.realPoint = new Point(unit1.iX * 32, unit1.iY * 32);
                                InRange();
                                Enemy_moving1.Stop();
                                ListOfMovingEnemies[0] = new Point(-1, -1);
                            }
                        }

                    }
                }
            }
            if (ListOfEnemyPathes[0].Count() == 0)
            {
                Enemy_moving1.Stop();
                ListOfMovingEnemies[0] = new Point(-1, -1);
            }
        }

        private void Enemy_moving2_Tick(object sender, EventArgs e)
        {
            if (ListOfEnemyPathes[1].Count > 0)
            {
                for (int i = 0; i < Enemies.Count; ++i)
                {
                    if (Enemies[i].iX == ListOfMovingEnemies[1].X && Enemies[i].iY == ListOfMovingEnemies[1].Y)
                    {
                        int side = 0;
                        Unit unit1 = Enemies[i];
                        Point Nextpoint = ListOfEnemyPathes[1][0];
                        /*bool busy = false;
                        for(int j = 0; j < ActiveElements.Count; ++j)
                        {
                            if (ActiveElements[j].Type == "Unit")
                            {
                                Unit busyUnit = ActiveElements[j] as Unit;
                                if(busyUnit.realPoint.X - 2 <= unit1.realPoint.X && busyUnit.realPoint.X + 2 >= unit1.realPoint.X && busyUnit.realPoint.Y - 2 <= unit1.realPoint.Y && busyUnit.realPoint.Y + 2 >= unit1.realPoint.Y && unit1.iX != busyUnit.iX && unit1.iY != busyUnit.iY)
                                {
                                    busy = true;
                                }
                            }
                        }*/
                        if (Blocks[Nextpoint.X, Nextpoint.Y] == 0 /*&& !busy*/)
                        {
                            if (unit1.iX == Nextpoint.X && unit1.iY != Nextpoint.Y)
                            {
                                if (unit1.iY > Nextpoint.Y)
                                {
                                    unit1.realPoint = new PointF(unit1.realPoint.X, unit1.realPoint.Y - 6.4f);
                                    side = 1;
                                }
                                else
                                {
                                    unit1.realPoint = new PointF(unit1.realPoint.X, unit1.realPoint.Y + 6.4f);
                                    side = 5;
                                }
                            }
                            else if (unit1.iY == Nextpoint.Y && unit1.iX != Nextpoint.X)
                            {
                                if (unit1.iX > Nextpoint.X)
                                {
                                    unit1.realPoint = new PointF(unit1.realPoint.X - 6.4f, unit1.realPoint.Y);
                                    side = -3;
                                }
                                else
                                {
                                    unit1.realPoint = new PointF(unit1.realPoint.X + 6.4f, unit1.realPoint.Y);
                                    side = 3;
                                }
                            }
                            else if (unit1.iX != Nextpoint.X && unit1.iY != Nextpoint.Y)
                            {
                                if (unit1.iX > Nextpoint.X && unit1.iY > Nextpoint.Y)
                                {
                                    unit1.realPoint = new PointF(unit1.realPoint.X - 6.4f, unit1.realPoint.Y - 6.4f);
                                    side = -2;
                                }
                                else if (unit1.iX > Nextpoint.X && unit1.iY < Nextpoint.Y)
                                {
                                    unit1.realPoint = new PointF(unit1.realPoint.X - 6.4f, unit1.realPoint.Y + 6.4f);
                                    side = -4;
                                }
                                else if (unit1.iX < Nextpoint.X && unit1.iY > Nextpoint.Y)
                                {
                                    unit1.realPoint = new PointF(unit1.realPoint.X + 6.4f, unit1.realPoint.Y - 6.4f);
                                    side = 2;
                                }
                                else if (unit1.iX < Nextpoint.X && unit1.iY < Nextpoint.Y)
                                {
                                    unit1.realPoint = new PointF(unit1.realPoint.X + 6.4f, unit1.realPoint.Y + 6.4f);
                                    side = 4;
                                }
                            }
                            IndicesOfEnemyPathes[1]++;
                            unit1.Frame = GetBMPofFrame(side, IndicesOfEnemyPathes[1], unit1.BlockIndice);
                            if (IndicesOfEnemyPathes[1] == 5)
                            {
                                Blocks[unit1.iX, unit1.iY] = 0;
                                unit1.iX = Nextpoint.X;
                                unit1.iY = Nextpoint.Y;
                                unit1.realPoint = new Point(unit1.iX * 32, unit1.iY * 32);
                                if (unit1.BlockIndice == 12)
                                    Blocks[Nextpoint.X, Nextpoint.Y] = 12;
                                if (unit1.BlockIndice == 13)
                                    Blocks[Nextpoint.X, Nextpoint.Y] = 13;
                                ListOfMovingEnemies[1] = Nextpoint;
                                IndicesOfEnemyPathes[1] = 0;
                                ListOfEnemyPathes[1].RemoveAt(0);
                            }
                            Enemies.RemoveAt(i);
                            Enemies.Add(unit1);
                            InRange();
                            pictureBox1.Refresh();
                            miniMap.Refresh();
                            break;
                        }
                        else if (Blocks[Nextpoint.X, Nextpoint.Y] == 0 || Blocks[Nextpoint.X, Nextpoint.Y] == 5 || Blocks[Nextpoint.X, Nextpoint.Y] == 2 || Blocks[Nextpoint.X, Nextpoint.Y] == 6)
                        {
                            for (int j = 0; j < AllBuildings.Count; ++j)
                            {
                                if (Nextpoint.X >= AllBuildings[j].iX && Nextpoint.X <= AllBuildings[j].iX + AllBuildings[j].Size - 1 && Nextpoint.Y >= AllBuildings[j].iY && Nextpoint.Y <= AllBuildings[j].iY + AllBuildings[j].Size - 1)
                                {
                                    Enemies[i].buildingtarget = AllBuildings[j];
                                }
                            }
                            StoppedUnit(Enemies[i]);
                        }
                        else
                        {
                            List<Point> newPath = FindPath(Blocks, new Point(unit1.iX, unit1.iY), ListOfEnemyPathes[1].Last(), unit1.BlockIndice);
                            if (newPath != null)
                            {
                                unit1.realPoint = new Point(unit1.iX * 32, unit1.iY * 32);
                                newPath.RemoveAt(0);
                                ListOfEnemyPathes[1] = newPath;
                            }
                            else
                            {
                                unit1.realPoint = new Point(unit1.iX * 32, unit1.iY * 32);
                                InRange();
                                Enemy_moving2.Stop();
                                ListOfMovingEnemies[1] = new Point(-1, -1);
                            }
                        }

                    }
                }
            }
            if (ListOfEnemyPathes[1].Count() == 0)
            {
                Enemy_moving2.Stop();
                ListOfMovingEnemies[1] = new Point(-1, -1);
            }
        }

        private void Enemy_moving3_Tick(object sender, EventArgs e)
        {
            if (ListOfEnemyPathes[2].Count > 0)
            {
                for (int i = 0; i < Enemies.Count; ++i)
                {
                    if (Enemies[i].iX == ListOfMovingEnemies[2].X && Enemies[i].iY == ListOfMovingEnemies[2].Y)
                    {
                        int side = 0;
                        Unit unit1 = Enemies[i];
                        Point Nextpoint = ListOfEnemyPathes[2][0];
                        /*bool busy = false;
                        for(int j = 0; j < ActiveElements.Count; ++j)
                        {
                            if (ActiveElements[j].Type == "Unit")
                            {
                                Unit busyUnit = ActiveElements[j] as Unit;
                                if(busyUnit.realPoint.X - 2 <= unit1.realPoint.X && busyUnit.realPoint.X + 2 >= unit1.realPoint.X && busyUnit.realPoint.Y - 2 <= unit1.realPoint.Y && busyUnit.realPoint.Y + 2 >= unit1.realPoint.Y && unit1.iX != busyUnit.iX && unit1.iY != busyUnit.iY)
                                {
                                    busy = true;
                                }
                            }
                        }*/
                        if (Blocks[Nextpoint.X, Nextpoint.Y] == 0 /*&& !busy*/)
                        {
                            if (unit1.iX == Nextpoint.X && unit1.iY != Nextpoint.Y)
                            {
                                if (unit1.iY > Nextpoint.Y)
                                {
                                    unit1.realPoint = new PointF(unit1.realPoint.X, unit1.realPoint.Y - 6.4f);
                                    side = 1;
                                }
                                else
                                {
                                    unit1.realPoint = new PointF(unit1.realPoint.X, unit1.realPoint.Y + 6.4f);
                                    side = 5;
                                }
                            }
                            else if (unit1.iY == Nextpoint.Y && unit1.iX != Nextpoint.X)
                            {
                                if (unit1.iX > Nextpoint.X)
                                {
                                    unit1.realPoint = new PointF(unit1.realPoint.X - 6.4f, unit1.realPoint.Y);
                                    side = -3;
                                }
                                else
                                {
                                    unit1.realPoint = new PointF(unit1.realPoint.X + 6.4f, unit1.realPoint.Y);
                                    side = 3;
                                }
                            }
                            else if (unit1.iX != Nextpoint.X && unit1.iY != Nextpoint.Y)
                            {
                                if (unit1.iX > Nextpoint.X && unit1.iY > Nextpoint.Y)
                                {
                                    unit1.realPoint = new PointF(unit1.realPoint.X - 6.4f, unit1.realPoint.Y - 6.4f);
                                    side = -2;
                                }
                                else if (unit1.iX > Nextpoint.X && unit1.iY < Nextpoint.Y)
                                {
                                    unit1.realPoint = new PointF(unit1.realPoint.X - 6.4f, unit1.realPoint.Y + 6.4f);
                                    side = -4;
                                }
                                else if (unit1.iX < Nextpoint.X && unit1.iY > Nextpoint.Y)
                                {
                                    unit1.realPoint = new PointF(unit1.realPoint.X + 6.4f, unit1.realPoint.Y - 6.4f);
                                    side = 2;
                                }
                                else if (unit1.iX < Nextpoint.X && unit1.iY < Nextpoint.Y)
                                {
                                    unit1.realPoint = new PointF(unit1.realPoint.X + 6.4f, unit1.realPoint.Y + 6.4f);
                                    side = 4;
                                }
                            }
                            IndicesOfEnemyPathes[2]++;
                            unit1.Frame = GetBMPofFrame(side, IndicesOfEnemyPathes[2], unit1.BlockIndice);
                            if (IndicesOfEnemyPathes[2] == 5)
                            {
                                Blocks[unit1.iX, unit1.iY] = 0;
                                unit1.iX = Nextpoint.X;
                                unit1.iY = Nextpoint.Y;
                                unit1.realPoint = new Point(unit1.iX * 32, unit1.iY * 32);
                                if (unit1.BlockIndice == 12)
                                    Blocks[Nextpoint.X, Nextpoint.Y] = 12;
                                if (unit1.BlockIndice == 13)
                                    Blocks[Nextpoint.X, Nextpoint.Y] = 13;
                                ListOfMovingEnemies[2] = Nextpoint;
                                IndicesOfEnemyPathes[2] = 0;
                                ListOfEnemyPathes[2].RemoveAt(0);
                            }
                            Enemies.RemoveAt(i);
                            Enemies.Add(unit1);
                            InRange();
                            pictureBox1.Refresh();
                            miniMap.Refresh();
                            break;
                        }
                        else if (Blocks[Nextpoint.X, Nextpoint.Y] == 0 || Blocks[Nextpoint.X, Nextpoint.Y] == 5 || Blocks[Nextpoint.X, Nextpoint.Y] == 2 || Blocks[Nextpoint.X, Nextpoint.Y] == 6)
                        {
                            for (int j = 0; j < AllBuildings.Count; ++j)
                            {
                                if (Nextpoint.X >= AllBuildings[j].iX && Nextpoint.X <= AllBuildings[j].iX + AllBuildings[j].Size - 1 && Nextpoint.Y >= AllBuildings[j].iY && Nextpoint.Y <= AllBuildings[j].iY + AllBuildings[j].Size - 1)
                                {
                                    Enemies[i].buildingtarget = AllBuildings[j];
                                }
                            }
                            StoppedUnit(Enemies[i]);
                        }
                        else
                        {
                            List<Point> newPath = FindPath(Blocks, new Point(unit1.iX, unit1.iY), ListOfEnemyPathes[2].Last(), unit1.BlockIndice);
                            if (newPath != null)
                            {
                                unit1.realPoint = new Point(unit1.iX * 32, unit1.iY * 32);
                                newPath.RemoveAt(0);
                                ListOfEnemyPathes[2] = newPath;
                            }
                            else
                            {
                                unit1.realPoint = new Point(unit1.iX * 32, unit1.iY * 32);
                                InRange();
                                Enemy_moving3.Stop();
                                ListOfMovingEnemies[2] = new Point(-1, -1);
                            }
                        }

                    }
                }
            }
            if (ListOfEnemyPathes[2].Count() == 0)
            {
                Enemy_moving3.Stop();
                ListOfMovingEnemies[2] = new Point(-1, -1);
            }
        }

        private void Enemy_moving4_Tick(object sender, EventArgs e)
        {
            if (ListOfEnemyPathes[3].Count > 0)
            {
                for (int i = 0; i < Enemies.Count; ++i)
                {
                    if (Enemies[i].iX == ListOfMovingEnemies[3].X && Enemies[i].iY == ListOfMovingEnemies[3].Y)
                    {
                        int side = 0;
                        Unit unit1 = Enemies[i];
                        Point Nextpoint = ListOfEnemyPathes[3][0];
                        /*bool busy = false;
                        for(int j = 0; j < ActiveElements.Count; ++j)
                        {
                            if (ActiveElements[j].Type == "Unit")
                            {
                                Unit busyUnit = ActiveElements[j] as Unit;
                                if(busyUnit.realPoint.X - 2 <= unit1.realPoint.X && busyUnit.realPoint.X + 2 >= unit1.realPoint.X && busyUnit.realPoint.Y - 2 <= unit1.realPoint.Y && busyUnit.realPoint.Y + 2 >= unit1.realPoint.Y && unit1.iX != busyUnit.iX && unit1.iY != busyUnit.iY)
                                {
                                    busy = true;
                                }
                            }
                        }*/
                        if (Blocks[Nextpoint.X, Nextpoint.Y] == 0 /*&& !busy*/)
                        {
                            if (unit1.iX == Nextpoint.X && unit1.iY != Nextpoint.Y)
                            {
                                if (unit1.iY > Nextpoint.Y)
                                {
                                    unit1.realPoint = new PointF(unit1.realPoint.X, unit1.realPoint.Y - 6.4f);
                                    side = 1;
                                }
                                else
                                {
                                    unit1.realPoint = new PointF(unit1.realPoint.X, unit1.realPoint.Y + 6.4f);
                                    side = 5;
                                }
                            }
                            else if (unit1.iY == Nextpoint.Y && unit1.iX != Nextpoint.X)
                            {
                                if (unit1.iX > Nextpoint.X)
                                {
                                    unit1.realPoint = new PointF(unit1.realPoint.X - 6.4f, unit1.realPoint.Y);
                                    side = -3;
                                }
                                else
                                {
                                    unit1.realPoint = new PointF(unit1.realPoint.X + 6.4f, unit1.realPoint.Y);
                                    side = 3;
                                }
                            }
                            else if (unit1.iX != Nextpoint.X && unit1.iY != Nextpoint.Y)
                            {
                                if (unit1.iX > Nextpoint.X && unit1.iY > Nextpoint.Y)
                                {
                                    unit1.realPoint = new PointF(unit1.realPoint.X - 6.4f, unit1.realPoint.Y - 6.4f);
                                    side = -2;
                                }
                                else if (unit1.iX > Nextpoint.X && unit1.iY < Nextpoint.Y)
                                {
                                    unit1.realPoint = new PointF(unit1.realPoint.X - 6.4f, unit1.realPoint.Y + 6.4f);
                                    side = -4;
                                }
                                else if (unit1.iX < Nextpoint.X && unit1.iY > Nextpoint.Y)
                                {
                                    unit1.realPoint = new PointF(unit1.realPoint.X + 6.4f, unit1.realPoint.Y - 6.4f);
                                    side = 2;
                                }
                                else if (unit1.iX < Nextpoint.X && unit1.iY < Nextpoint.Y)
                                {
                                    unit1.realPoint = new PointF(unit1.realPoint.X + 6.4f, unit1.realPoint.Y + 6.4f);
                                    side = 4;
                                }
                            }
                            IndicesOfEnemyPathes[3]++;
                            unit1.Frame = GetBMPofFrame(side, IndicesOfEnemyPathes[3], unit1.BlockIndice);
                            if (IndicesOfEnemyPathes[3] == 5)
                            {
                                Blocks[unit1.iX, unit1.iY] = 0;
                                unit1.iX = Nextpoint.X;
                                unit1.iY = Nextpoint.Y;
                                unit1.realPoint = new Point(unit1.iX * 32, unit1.iY * 32);
                                if (unit1.BlockIndice == 12)
                                    Blocks[Nextpoint.X, Nextpoint.Y] = 12;
                                if (unit1.BlockIndice == 13)
                                    Blocks[Nextpoint.X, Nextpoint.Y] = 13;
                                ListOfMovingEnemies[3] = Nextpoint;
                                IndicesOfEnemyPathes[3] = 0;
                                ListOfEnemyPathes[3].RemoveAt(0);
                            }
                            Enemies.RemoveAt(i);
                            Enemies.Add(unit1);
                            InRange();
                            pictureBox1.Refresh();
                            miniMap.Refresh();
                            break;
                        }
                        else if (Blocks[Nextpoint.X, Nextpoint.Y] == 0 || Blocks[Nextpoint.X, Nextpoint.Y] == 5 || Blocks[Nextpoint.X, Nextpoint.Y] == 2 || Blocks[Nextpoint.X, Nextpoint.Y] == 6)
                        {
                            for (int j = 0; j < AllBuildings.Count; ++j)
                            {
                                if (Nextpoint.X >= AllBuildings[j].iX && Nextpoint.X <= AllBuildings[j].iX + AllBuildings[j].Size - 1 && Nextpoint.Y >= AllBuildings[j].iY && Nextpoint.Y <= AllBuildings[j].iY + AllBuildings[j].Size - 1)
                                {
                                    Enemies[i].buildingtarget = AllBuildings[j];
                                }
                            }
                            StoppedUnit(Enemies[i]);
                        }
                        else
                        {
                            List<Point> newPath = FindPath(Blocks, new Point(unit1.iX, unit1.iY), ListOfEnemyPathes[3].Last(), unit1.BlockIndice);
                            if (newPath != null)
                            {
                                unit1.realPoint = new Point(unit1.iX * 32, unit1.iY * 32);
                                newPath.RemoveAt(0);
                                ListOfEnemyPathes[3] = newPath;
                            }
                            else
                            {
                                unit1.realPoint = new Point(unit1.iX * 32, unit1.iY * 32);
                                InRange();
                                Enemy_moving4.Stop();
                                ListOfMovingEnemies[3] = new Point(-1, -1);
                            }
                        }

                    }
                }
            }
            if (ListOfEnemyPathes[3].Count() == 0)
            {
                Enemy_moving4.Stop();
                ListOfMovingEnemies[3] = new Point(-1, -1);
            }
        }
    }
}

