using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace akademine_sistema
{
    public partial class studentas : Form
    {
        private readonly string connectionString = "Server=localhost;Database=akademine_sistema;Uid=root;Pwd=;";
        private int studentId;

        public studentas(int studentId)
        {
            InitializeComponent();
            this.studentId = studentId;

            Load += Studentas_Load;
        }

        private void Studentas_Load(object sender, EventArgs e)
        {
            try
            {
                LoadStudentGrades(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Įvyko klaida kraunant pažymius: {ex.Message}", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadStudentGrades()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string query = @"
                        SELECT 
                            d.pavadinimas AS 'Dalykas',
                            GROUP_CONCAT(p.pazymys ORDER BY p.data SEPARATOR ', ') AS 'Pažymiai'
                        FROM 
                            pazymiai p
                        JOIN 
                            dalykai d ON p.dalyko_id = d.id
                        WHERE 
                            p.studento_id = @studentId
                        GROUP BY 
                            d.pavadinimas";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@studentId", studentId);

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            dgvPazymiai.DataSource = dt; 
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Nepavyko užkrauti pažymių: {ex.Message}", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void Atsijungti_Click(object sender, EventArgs e)
        {
                Form1 loginForm = new Form1();
                loginForm.Show();
                this.Close();
        }
    }
}
