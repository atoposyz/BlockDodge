using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace demo
{
    public partial class Form2 : Form
    {
        private const string RANK_FILENAME = "rank.txt";
        //private const int RANK_COUNT = 10; 


        public Form2()
        {
            InitializeComponent();

            List<RankItem> rankList = ReadRankDataFromFile(RANK_FILENAME);

            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = rankList;
            //dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

        }
        private List<RankItem> ReadRankDataFromFile(string fileName)
        {
            List<RankItem> rankList = new List<RankItem>();

            using (StreamReader sr = new StreamReader(fileName))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] fields = line.Split(',');
                    if (fields.Length == 2)
                    {
                        string name = fields[0];
                        int score = int.Parse(fields[1]);
                        rankList.Add(new RankItem(name, score));
                    }
                }
            }

            return rankList;
        }
        public static void AddOrUpdateRankItem(string name, int score)
        {
            List<string> lines = new List<string>();
            bool found = false;
            using (StreamReader sr = new StreamReader("rank.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length == 2 && int.TryParse(parts[1].Trim(), out int scoreValue))
                    {
                        lines.Add(line);
                        if (parts[0].Trim() == name)
                        {
                            scoreValue = Math.Max(scoreValue, score);
                            found = true;
                            lines[lines.Count - 1] = $"{name}, {scoreValue}";
                        }
                    }
                }
            }

            if (!found)
            {
                lines.Add($"{name}, {score}");
            }


            List<string> sortedLines = lines.OrderByDescending(x =>
            {
                int scoreValue = int.Parse(x.Split(',')[1].Trim());
                return scoreValue;
            }).ToList();


            using (StreamWriter sw = new StreamWriter("rank.txt", false))
            {
                foreach (string line in sortedLines)
                {
                    sw.WriteLine(line);
                }
            }
        }


    }

    public class RankItem
    {
        public string Name { get; set; }
        public int Score { get; set; }

        public RankItem(string name, int score)
        {
            Name = name;
            Score = score;
        }
    }



}
