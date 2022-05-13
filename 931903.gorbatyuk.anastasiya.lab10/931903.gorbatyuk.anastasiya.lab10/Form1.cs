using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _931903.gorbatyuk.anastasiya.lab10
{
    public partial class Form1 : Form
    {
        Random rnd = new Random();
        const int teamCount = 6;
        string[] teamName = new string[teamCount] {"Спартак", "Томь", "Динамо", "Челси", "Манчестер Юнайтет", "Реал Мадрит"};
        const double pointWin = 2;
        const double pointDraw = 1;
        const double pointLoss = 0;
        List<Match> MatchesInTournament;
        Team[] TeamsInTournament;
        
        struct Team
        {
            public string name;
            public int game;
            public int goal;
            public int win;
            public int draw;
            public int loss;
            public double points;
        }

        struct Match
        {
            public Team team1, team2;
            public int goal1, goal2;
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        { 
            // создаем новый турнир
            TeamsInTournament = new Team[teamCount];
            MatchesInTournament = new List<Match>();
            
            // присваиваем имена командам в ноом турнире
            for (int i = 0; i < teamCount; i++)
            {
                TeamsInTournament[i] = new Team() { name = teamName[i] };
            }
            // проводим матчи
            //для каждой пары команд
            int goalPlayer1, goalPlayer2;
            for (int Player1 = 0; Player1 < teamCount; Player1++) {
                for (int Player2 = 0; Player2 < teamCount; Player2++) {
                    if (Player1 != Player2) // исключаем игру команд с самими собою
                    {
                        goalPlayer1 = GoalsInMatch(rnd.NextDouble()); //генерируем количество голов 
                        goalPlayer2 = GoalsInMatch(rnd.NextDouble());

                        Match newMatch = new Match() // проводим игру
                        {
                            team1 = TeamsInTournament[Player1],
                            team2 = TeamsInTournament[Player2],
                            goal1 = goalPlayer1,
                            goal2 = goalPlayer2
                        };
                        MatchesInTournament.Add(newMatch); // добавляем матч в таблицу

                        //заполняем таблицу

                        //количество голов
                        TeamsInTournament[Player1].goal += goalPlayer1;
                        TeamsInTournament[Player2].goal += goalPlayer2;

                        // количество игр
                        TeamsInTournament[Player1].game++;
                        TeamsInTournament[Player2].game++;

                        // победа прогрыш ничья
                        if (goalPlayer1 > goalPlayer2)
                        {
                            TeamsInTournament[Player1].win++;
                            TeamsInTournament[Player2].loss++;
                        }
                        else if (goalPlayer1 < goalPlayer2)
                        {
                            TeamsInTournament[Player1].loss++;
                            TeamsInTournament[Player2].win++;
                        }
                        else
                        {
                            TeamsInTournament[Player1].draw++;
                            TeamsInTournament[Player2].draw++;
                        }
                    }
                }
            }

            // подсчет очков
            for (int i = 0; i < teamCount; i++)
            {
                TeamsInTournament[i].points = TeamsInTournament[i].win * pointWin + TeamsInTournament[i].draw * pointDraw + TeamsInTournament[i].loss * pointLoss;
            }

            //выводим таблицу результатов
            for (int i = 0; i < teamCount; i++)
            {
                dataGridResult.Rows.Add(
                    TeamsInTournament[i].name,
                    TeamsInTournament[i].game,
                    TeamsInTournament[i].win,
                    TeamsInTournament[i].draw,
                    TeamsInTournament[i].loss,
                    TeamsInTournament[i].goal,
                    TeamsInTournament[i].points
                );
            }

            //выводим таблицу матчей
            foreach (var match in MatchesInTournament)
            {
                dataGridMatch.Rows.Add(
                    match.team1.name,
                    match.team2.name,
                    $"{ match.goal1 }:{ match.goal2 }"
                );
            }
        }


        // метод для подcчета забитых голов
        private int GoalsInMatch(double lyamda)
        {
            int m = 0;
            double S = Math.Log(rnd.NextDouble() * 10);

            while (S > -lyamda)
            {
                S += Math.Log(rnd.NextDouble());
                m++;
            }

            return m;
        }

        private void dataGridResult_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
