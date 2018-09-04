using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace FinalFantasyI
{
    public class Repository
    {
        SQLiteConnection sqliteConn = new SQLiteConnection(@"Data Source=C:\Users\gribe\Documents\coisas Gabriel\databases\Final_Fantasy_I.db");
        string GetAllQuery = @"SELECT id, name FROM Job";

        public List<T> GetAll<T>(string tableName) where T : Entity
        {
            List<T> entidades = new List<T>();

            string query = "SELECT id, name FROM " + tableName;

            SQLiteCommand cmd = new SQLiteCommand(query, sqliteConn);
            sqliteConn.Open();
            SQLiteDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var elemento = new {
                    Id = Convert.ToInt32(reader["id"]),
                    Name = reader["name"].ToString()
                } as T;

                entidades.Add(elemento);
            }

            return entidades;
        }



        public List<Job> GetJobs()
        {
            List<Job> jobs = new List<Job>();
            string query = "SELECT id, name FROM Job";

            SQLiteCommand cmd = new SQLiteCommand(query, sqliteConn);

            sqliteConn.Open();
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Job job = new Job()
                {
                    Id = Convert.ToInt32(reader["id"]),
                    Name = reader["name"].ToString()
                };

                //WEAPON
                string queryweapon = "SELECT jobId, weaponId FROM JobWeapon";

                SQLiteCommand cmdweapon = new SQLiteCommand(queryweapon, sqliteConn);
                SQLiteDataReader readerweapon = cmdweapon.ExecuteReader();
                while (readerweapon.Read())
                {
                    JobWeapon jobweapon = new JobWeapon();
                    if (reader["id"] == readerweapon["jobId"])
                    {
                        jobweapon.JobId = Convert.ToInt32(readerweapon["jobId"]);
                        jobweapon.WeaponId = Convert.ToInt32(readerweapon["weaponId"]);
                        job.JobWeapon.Add(jobweapon);
                    }
                }

                //ARMOR
                string queryarmor = "SELECT jobId, armorId FROM JobArmor";

                SQLiteCommand cmdarmor = new SQLiteCommand(queryarmor, sqliteConn);
                SQLiteDataReader readerarmor = cmdarmor.ExecuteReader();
                while (readerarmor.Read())
                {
                    JobArmor jobarmor = new JobArmor();
                    if (reader["id"] == readerarmor["jobId"])
                    {
                        jobarmor.JobId = Convert.ToInt32(readerarmor["jobId"]);
                        jobarmor.ArmorId = Convert.ToInt32(readerarmor["armorId"]);
                        job.JobArmor.Add(jobarmor);
                    }
                }
                jobs.Add(job);
            }
            sqliteConn.Close();
            return jobs;
        }
        public List<Weapon> GetWeapons()
        {
            List<Weapon> weapons = new List<Weapon>();
            string query = "SELECT id, name, damage, hit FROM Weapon";

            SQLiteCommand cmd = new SQLiteCommand(query, sqliteConn);

            sqliteConn.Open();
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Weapon weapon = new Weapon()
                {
                    Id = Convert.ToInt32(reader["id"]),
                    Name = reader["name"].ToString(),
                    Damage = Convert.ToInt32(reader["damage"]),
                    Hit = Convert.ToInt32(reader["hit"])
                };
                weapons.Add(weapon);
            }
            sqliteConn.Close();
            return weapons;
        }
        public List<Armor> GetArmors()
        {
            List<Armor> armors = new List<Armor>();
            string query = "SELECT id, name, absorb, evade, typeId FROM Armor";

            SQLiteCommand cmd = new SQLiteCommand(query, sqliteConn);

            sqliteConn.Open();
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Armor armor = new Armor()
                {
                    Id = Convert.ToInt32(reader["id"]),
                    Name = reader["name"].ToString(),
                    Absorb = Convert.ToInt32(reader["absorb"]),
                    Evade = Convert.ToInt32(reader["evade"]),
                    ArmorType = (ArmorType)Convert.ToInt32(reader["typeId"])
                };
                armors.Add(armor);
            }
            sqliteConn.Close();
            return armors;
        }
        public List<JobWeaponArmor> GetJobWeaponArmos()
        {
            List<JobWeaponArmor> jobWeaponArmos = new List<JobWeaponArmor>();
            string query = "select Job.name, Weapon.name, Armor.name from Job ";
            query += "join JobWeapon on Job.id = JobWeapon.jobId ";
            query += "join Weapon on JobWeapon.weaponId = Weapon.id ";
            query += "join JobArmor on Job.id = JobArmor.jobId ";
            query += "join Armor on JobArmor.armorId = Armor.id";

            SQLiteCommand cmd = new SQLiteCommand(query, sqliteConn);

            sqliteConn.Open();
            SQLiteDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                JobWeaponArmor jobweaponarmors = new JobWeaponArmor()
                {
                    job = reader[0].ToString(),
                    jobweapon = reader[1].ToString(),
                    jobarmor = reader[2].ToString()
                };
                jobWeaponArmos.Add(jobweaponarmors);
            }
            sqliteConn.Close();
            return jobWeaponArmos;
        }
        public void AddJob(string job)
        {
            string query = "INSERT INTO Job (name) values ('" + job + "');";
            SQLiteCommand cmd = new SQLiteCommand(query, sqliteConn);
            sqliteConn.Open();
            cmd.ExecuteNonQuery();
            sqliteConn.Close();
        }
        public bool DeleteJob(string job)
        {
            bool result = true;
            string query = "DELETE FROM Job WHERE name='" + job + "';";
            SQLiteCommand cmd = new SQLiteCommand(query, sqliteConn);
            sqliteConn.Open();
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch
            {
                result = false;
            }
            sqliteConn.Close();
            return result;
        }
        public void AddArmor(string armor, int armortype, int absorb, int evade)
        {
            string query = "insert into Armor (name, absorb, evade, typeId) values ('" + armor + "', ";
            query += absorb + ", " + evade + ", " + armortype + ");";
            SQLiteCommand cmd = new SQLiteCommand(query, sqliteConn);
            sqliteConn.Open();
            cmd.ExecuteNonQuery();
            sqliteConn.Close();
        }
        public bool DeleteArmor(string armor)
        {
            bool result = true;
            string query = "DELETE FROM Armor WHERE name='" + armor + "';";
            SQLiteCommand cmd = new SQLiteCommand(query, sqliteConn);
            sqliteConn.Open();
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch
            {
                result = false;
            }
            sqliteConn.Close();
            return result;
        }
        public void AddWeapon(string weapon, int damage, int hit)
        {
            string query = "insert into Weapon (name, damage, hit) ";
            query += "values ('" + weapon + "', " + damage + ", " + hit + ");";
            SQLiteCommand cmd = new SQLiteCommand(query, sqliteConn);
            sqliteConn.Open();
            cmd.ExecuteNonQuery();
            sqliteConn.Close();
        }
        public bool DeleteWeapon(string weapon)
        {
            bool result = true;
            string query = "DELETE FROM Weapon WHERE name='" + weapon + "';";
            SQLiteCommand cmd = new SQLiteCommand(query, sqliteConn);
            sqliteConn.Open();
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch
            {
                result = false;
            }
            sqliteConn.Close();
            return result;
        }
    }
}
