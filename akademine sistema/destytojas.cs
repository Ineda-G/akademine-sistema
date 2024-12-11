using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace akademine_sistema
{
    public partial class destytojas : Form
    {
        private readonly string connectionString = "Server=localhost;Database=akademine_sistema;Uid=root;Pwd=;";

        public destytojas()
        {
            InitializeComponent();

            cmbStudentai.DropDownStyle = ComboBoxStyle.DropDownList;

            Load += Destytojas_Load;
        }

        private void Destytojas_Load(object sender, EventArgs e)
        {
            try
            {
                LoadAllStudents(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Įvyko klaida kraunant studentus: {ex.Message}", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadAllStudents()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string query = "SELECT id, CONCAT(vardas, ' ', pavarde) AS full_name FROM studentai";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        cmbStudentai.Items.Clear(); 

                        if (!reader.HasRows)
                        {
                            MessageBox.Show("Studentų sąrašas yra tuščias.", "Informacija", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        while (reader.Read())
                        {
                            int studentId = Convert.ToInt32(reader["id"]); 
                            string fullName = reader["full_name"].ToString(); 

                            cmbStudentai.Items.Add(new KeyValuePair<int, string>(studentId, fullName));
                        }
                    }

                    cmbStudentai.DisplayMember = "Value"; 
                    cmbStudentai.ValueMember = "Key"; 
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Nepavyko užkrauti studentų: {ex.Message}", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cmbStudentai_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbStudentai.SelectedItem == null)
            {
                MessageBox.Show("Prašome pasirinkti studentą.", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var selectedStudent = (KeyValuePair<int, string>)cmbStudentai.SelectedItem;
            int studentId = selectedStudent.Key;

            LoadStudentGrades(studentId);
        }

        private void LoadStudentGrades(int studentId)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string query = @"
                        SELECT 
                            p.id AS 'Pažymio ID', 
                            p.dalykas AS 'Dalykas', 
                            p.pazymys AS 'Pažymys', 
                            p.data AS 'Data'
                        FROM 
                            pazymiai p
                        WHERE 
                            p.studento_id = @studentoId";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@studentoId", studentId);

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            adapter.Fill(dt);

                            dgvPazymiai.DataSource = dt; 
                            dgvPazymiai.Columns["Pažymio ID"].Visible = false; 
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Nepavyko užkrauti pažymių: {ex.Message}", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnPakeisti_Click(object sender, EventArgs e)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    foreach (DataGridViewRow row in dgvPazymiai.Rows)
                    {
                        if (row.IsNewRow) continue; 

                        int pazymioId = Convert.ToInt32(row.Cells["Pažymio ID"].Value);
                        string dalykas = row.Cells["Dalykas"].Value.ToString();
                        int pazymys = Convert.ToInt32(row.Cells["Pažymys"].Value);
                        DateTime data = Convert.ToDateTime(row.Cells["Data"].Value);

                        string query = @"
                            UPDATE pazymiai
                            SET 
                                dalykas = @dalykas,
                                pazymys = @pazymys,
                                data = @data
                            WHERE 
                                id = @pazymioId";

                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@dalykas", dalykas);
                            cmd.Parameters.AddWithValue("@pazymys", pazymys);
                            cmd.Parameters.AddWithValue("@data", data);
                            cmd.Parameters.AddWithValue("@pazymioId", pazymioId);

                            cmd.ExecuteNonQuery();
                        }
                    }

                    MessageBox.Show("Pažymiai sėkmingai atnaujinti.", "Sėkmė", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Nepavyko atnaujinti pažymių: {ex.Message}", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnIstrinti_Click(object sender, EventArgs e)
        {
 
                if (dgvPazymiai.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Prašome pasirinkti pažymį, kurį norite ištrinti.", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int pazymioId = Convert.ToInt32(dgvPazymiai.SelectedRows[0].Cells["Pažymio ID"].Value);

                DialogResult result = MessageBox.Show("Ar tikrai norite ištrinti šį pažymį?", "Patvirtinimas", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        try
                        {
                            conn.Open();

                            string query = "DELETE FROM pazymiai WHERE id = @pazymioId";

                            using (MySqlCommand cmd = new MySqlCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@pazymioId", pazymioId);
                                cmd.ExecuteNonQuery();
                            }

                            int studentId = ((KeyValuePair<int, string>)cmbStudentai.SelectedItem).Key;
                            LoadStudentGrades(studentId);

                            MessageBox.Show("Pažymys sėkmingai ištrintas.", "Sėkmė", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Nepavyko ištrinti pažymio: {ex.Message}", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
        }

        private void btnPrideti_Click(object sender, EventArgs e)
        {
                if (cmbStudentai.SelectedItem == null)
                {
                    MessageBox.Show("Prašome pasirinkti studentą.", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var selectedStudent = (KeyValuePair<int, string>)cmbStudentai.SelectedItem;
                int studentId = selectedStudent.Key;

                foreach (DataGridViewRow row in dgvPazymiai.Rows)
                {
                    if (row.IsNewRow) continue; 

                    if (row.Cells["Dalykas"].Value == null ||
                        row.Cells["Pažymys"].Value == null ||
                        row.Cells["Data"].Value == null)
                    {
                        MessageBox.Show("Visos eilutės reikšmės turi būti užpildytos.", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        continue;
                    }

                    string dalykas = row.Cells["Dalykas"].Value.ToString();
                    if (!int.TryParse(row.Cells["Pažymys"].Value.ToString(), out int pazymys) || pazymys < 1 || pazymys > 10)
                    {
                        MessageBox.Show("Pažymys turi būti tarp 1 ir 10.", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        continue;
                    }

                    if (!DateTime.TryParse(row.Cells["Data"].Value.ToString(), out DateTime data))
                    {
                        MessageBox.Show("Netinkama data.", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        continue;
                    }

                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        try
                        {
                            conn.Open();

                            string checkQuery = @"
                    SELECT COUNT(*) 
                    FROM pazymiai 
                    WHERE studento_id = @studentoId 
                        AND dalykas = @dalykas 
                        AND data = @data";

                            using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn))
                            {
                                checkCmd.Parameters.AddWithValue("@studentoId", studentId);
                                checkCmd.Parameters.AddWithValue("@dalykas", dalykas);
                                checkCmd.Parameters.AddWithValue("@data", data);

                                int count = Convert.ToInt32(checkCmd.ExecuteScalar());
                                if (count > 0)
                                {
                                    MessageBox.Show($"Pažymys už '{dalykas}' jau egzistuoja šiai dienai.", "Informacija", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    continue;
                                }
                            }

                            string insertQuery = @"
                    INSERT INTO pazymiai (studento_id, dalykas, pazymys, data)
                    VALUES (@studentoId, @dalykas, @pazymys, @data)";

                            using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, conn))
                            {
                                insertCmd.Parameters.AddWithValue("@studentoId", studentId);
                                insertCmd.Parameters.AddWithValue("@dalykas", dalykas);
                                insertCmd.Parameters.AddWithValue("@pazymys", pazymys);
                                insertCmd.Parameters.AddWithValue("@data", data);

                                insertCmd.ExecuteNonQuery();
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Nepavyko pridėti pažymio: {ex.Message}", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

                LoadStudentGrades(studentId);
                MessageBox.Show("Pažymys sėkmingai pridėtas.", "Sėkmė", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        private void Atsijungti_Click(object sender, EventArgs e)
        {
                Form1 loginForm = new Form1();
                loginForm.Show();
                this.Close();
        }
    }
}
