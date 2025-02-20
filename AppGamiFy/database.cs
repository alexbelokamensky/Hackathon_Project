﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Data.Sqlite;
using System.Threading.Tasks;

namespace APP_CIH_CAHUL_BAC
{
    public class Intrebare
    {
        public int Id { get; set; }
        public string questionText { get; set; }
        public string imageName { get; set; }
        public List<string> Raspunsuri { get; set; }
        public string Corecte { get; set; }

        public Intrebare(int id, string questionText, string imgName, string r1, string r2, string r3, string c1)
        {
            this.Id = id;
            this.questionText = questionText;
            this.imageName = imgName;
            this.Raspunsuri = new List<string> { r1, r2, r3 };
            this.Corecte = c1;
        }
        public Intrebare()
        {

        }
    }

    public class database1
    {
        private readonly string connectionString;
        //public string connectionString;
        public database1()
        {
            this.connectionString = "Data Source=bacalaureat_app_database.db";
        }
        public List<Intrebare> getData(string materie, int min, int max)
        {
            List<Intrebare> data = new List<Intrebare>();
            using (var connection = new SqliteConnection(this.connectionString))
            {
                connection.Open();
                //SqliteCommand command = new SqliteCommand("SELECT * FROM Intrebari",connection);
                SqliteCommand command = new SqliteCommand($"SELECT * FROM Intrebari{materie} where id>{min} and id<={max}", connection);
                SqliteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["ID"]);
                    string intrebare = Convert.ToString(reader["Intrebare"]);
                    string image = Convert.ToString(reader["Image"]);
                    string rs1 = Convert.ToString(reader["Raspuns1"]);
                    string rs2 = Convert.ToString(reader["Raspuns2"]);
                    string rs3 = Convert.ToString(reader["Raspuns3"]);
                    string c1 = Convert.ToString(reader["Corect"]);
                    data.Add(new Intrebare(id, intrebare, image, rs1, rs2, rs3, c1));
                }
                reader.Close();
                connection.Close();
            }
            return data;

        }
        public Intrebare getQuiz(string materie, int _id)
        {
            using (var connection = new SqliteConnection(this.connectionString))
            {
                connection.Open();
                //SqliteCommand command = new SqliteCommand("SELECT * FROM Intrebari",connection);
                SqliteCommand command = new SqliteCommand($"SELECT * FROM Intrebari{materie} where id={_id}", connection);
                SqliteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["ID"]);
                    string intrebare = Convert.ToString(reader["Intrebare"]);
                    string image = Convert.ToString(reader["Image"]);
                    string rs1 = Convert.ToString(reader["Raspuns1"]);
                    string rs2 = Convert.ToString(reader["Raspuns2"]);
                    string rs3 = Convert.ToString(reader["Raspuns3"]);
                    string c1 = Convert.ToString(reader["Corect"]);
                    return new Intrebare(id, intrebare, image, rs1, rs2, rs3, c1);
                }
                reader.Close();
                connection.Close();
            }
            return new Intrebare();
        }

    }
}
