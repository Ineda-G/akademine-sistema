using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace akademine_sistema
{
    public partial class Admin : Form
    {

        private readonly string connectionString = "Server=localhost;Database=akademine_sistema;Uid=root;Pwd=;";

        public Admin()
        {
            InitializeComponent();
            cmbSG.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbD.DropDownStyle = ComboBoxStyle.DropDownList;

            LoadStudentGroups();
            LoadSubjects();
            LoadLecturers();
            LoadStudents();
            LoadGroups();
        }

        private void LoadStudentGroups()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string query = "SELECT id, grupes_pavadinimas FROM studentu_grupes";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            cmbSG.Items.Clear();
                            cmbStudentasG.Items.Clear();

                            while (reader.Read())
                            {

                                int id = Convert.ToInt32(reader["id"]);
                                string groupName = reader["grupes_pavadinimas"].ToString();

                                cmbSG.Items.Add(groupName);
                                cmbStudentasG.Items.Add(new KeyValuePair<int, string>(id, groupName)); // ID ir pavadinimai
                            }
                        }
                    }

                    cmbStudentasG.DisplayMember = "Value";
                    cmbStudentasG.ValueMember = "Key";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Nepavyko užkrauti grupių: {ex.Message}", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadSubjects()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string query = "SELECT id, pavadinimas FROM dalykai";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            cmbD.Items.Clear();
                            cmbDestytojasD.Items.Clear();

                            while (reader.Read())
                            {
                                string dalykas = reader["pavadinimas"].ToString();
                                int dalykoId = Convert.ToInt32(reader["id"]);

                                cmbD.Items.Add(dalykas);
                                cmbDestytojasD.Items.Add(new KeyValuePair<int, string>(dalykoId, dalykas));
                            }
                        }
                    }

                    cmbDestytojasD.DisplayMember = "Value";
                    cmbDestytojasD.ValueMember = "Key";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Nepavyko užkrauti dalykų: {ex.Message}", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadLecturers()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string query = "SELECT id, CONCAT(vardas, ' ', pavarde) AS full_name FROM destytojai";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            cmbDestytojas.Items.Clear();

                            while (reader.Read())
                            {
                                cmbDestytojas.Items.Add(new KeyValuePair<int, string>(
                                    Convert.ToInt32(reader["id"]),
                                    reader["full_name"].ToString()
                                ));
                            }
                        }
                    }

                    cmbDestytojas.DisplayMember = "Value";
                    cmbDestytojas.ValueMember = "Key";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Nepavyko užkrauti dėstytojų: {ex.Message}", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void LoadStudents()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string query = "SELECT id, CONCAT(vardas, ' ', pavarde) AS full_name FROM studentai";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            cmbStudentas.Items.Clear();

                            while (reader.Read())
                            {
                                int id = Convert.ToInt32(reader["id"]);
                                string fullName = reader["full_name"].ToString();
                                cmbStudentas.Items.Add(new KeyValuePair<int, string>(id, fullName));
                            }
                        }
                    }

                    cmbStudentas.DisplayMember = "Value";
                    cmbStudentas.ValueMember = "Key";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Nepavyko užkrauti studentų: {ex.Message}", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void LoadGroups()
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string query = "SELECT id, grupes_pavadinimas FROM studentu_grupes";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            cmbStudentasG.Items.Clear();

                            while (reader.Read())
                            {
                                int id = Convert.ToInt32(reader["id"]);
                                string groupName = reader["grupes_pavadinimas"].ToString();
                                cmbStudentasG.Items.Add(new KeyValuePair<int, string>(id, groupName));
                            }
                        }
                    }

                    cmbStudentasG.DisplayMember = "Value";
                    cmbStudentasG.ValueMember = "Key";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Nepavyko užkrauti grupių: {ex.Message}", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void btnSalintiSG_Click(object sender, EventArgs e)
        {
            string grupesPavadinimas = cmbSG.SelectedItem?.ToString();

            if (string.IsNullOrWhiteSpace(grupesPavadinimas))
            {
                MessageBox.Show("Prašome pasirinkti grupę.", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult result = MessageBox.Show($"Ar tikrai norite pašalinti grupę '{grupesPavadinimas}'?",
                                                  "Patvirtinimas",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        string query = "DELETE FROM studentu_grupes WHERE grupes_pavadinimas = @grupesPavadinimas";
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@grupesPavadinimas", grupesPavadinimas);
                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Grupė sėkmingai pašalinta.", "Sėkmė", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadStudentGroups(); 
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Nepavyko pašalinti grupės: {ex.Message}", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnSukurtiSG_Click(object sender, EventArgs e)
        {
            string grupesPavadinimas = txtSG.Text.Trim();

            if (string.IsNullOrWhiteSpace(grupesPavadinimas))
            {
                MessageBox.Show("Prašome įvesti grupės pavadinimą.", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string query = "INSERT INTO studentu_grupes (grupes_pavadinimas) VALUES (@grupesPavadinimas)";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@grupesPavadinimas", grupesPavadinimas);
                        cmd.ExecuteNonQuery(); 

                        MessageBox.Show("Grupė sėkmingai sukurta.", "Sėkmė", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        txtSG.Clear();

                        LoadStudentGroups();
                    }
                }
                catch (MySqlException ex) when (ex.Number == 1062) // Klaida dėl dublikato
                {
                    MessageBox.Show("Tokia grupė jau egzistuoja.", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Nepavyko sukurti grupės: {ex.Message}", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnSukurtiD_Click(object sender, EventArgs e)
        {
                string dalykoPavadinimas = txtD.Text.Trim();

                if (string.IsNullOrWhiteSpace(dalykoPavadinimas))
                {
                    MessageBox.Show("Prašome įvesti dalyko pavadinimą.", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();

                        string query = "INSERT INTO dalykai (pavadinimas) VALUES (@pavadinimas)";
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@pavadinimas", dalykoPavadinimas);
                            cmd.ExecuteNonQuery();

                            MessageBox.Show("Dalykas sėkmingai sukurtas.", "Sėkmė", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtD.Clear(); 
                            LoadSubjects(); 
                        }
                    }
                    catch (MySqlException ex) when (ex.Number == 1062)
                    {
                        MessageBox.Show("Toks dalykas jau egzistuoja.", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Nepavyko sukurti dalyko: {ex.Message}", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
        }

        private void btnSalintiD_Click(object sender, EventArgs e)
        {
                string dalykoPavadinimas = cmbD.SelectedItem?.ToString();

                if (string.IsNullOrWhiteSpace(dalykoPavadinimas))
                {
                    MessageBox.Show("Prašome pasirinkti dalyką.", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult result = MessageBox.Show($"Ar tikrai norite pašalinti dalyką '{dalykoPavadinimas}'?",
                                                      "Patvirtinimas",
                                                      MessageBoxButtons.YesNo,
                                                      MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        try
                        {
                            conn.Open();

                            string query = "DELETE FROM dalykai WHERE pavadinimas = @pavadinimas";
                            using (MySqlCommand cmd = new MySqlCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@pavadinimas", dalykoPavadinimas);
                                cmd.ExecuteNonQuery();

                                MessageBox.Show("Dalykas sėkmingai pašalintas.", "Sėkmė", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadSubjects(); 
                                cmbD.SelectedIndex = -1; 
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Nepavyko pašalinti dalyko: {ex.Message}", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
        }

        private void BtnDSG_Click(object sender, EventArgs e)
        {
                string grupesPavadinimas = cmbSG.SelectedItem?.ToString();
                string dalykoPavadinimas = cmbD.SelectedItem?.ToString();

                if (string.IsNullOrWhiteSpace(grupesPavadinimas))
                {
                    MessageBox.Show("Prašome pasirinkti grupę.", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(dalykoPavadinimas))
                {
                    MessageBox.Show("Prašome pasirinkti dalyką.", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();

                        string grupesQuery = "SELECT id FROM studentu_grupes WHERE grupes_pavadinimas = @grupesPavadinimas";
                        int grupesId;
                        using (MySqlCommand grupesCmd = new MySqlCommand(grupesQuery, conn))
                        {
                            grupesCmd.Parameters.AddWithValue("@grupesPavadinimas", grupesPavadinimas);
                            object grupesResult = grupesCmd.ExecuteScalar();
                            if (grupesResult == null)
                            {
                                MessageBox.Show("Grupė nerasta.", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            grupesId = Convert.ToInt32(grupesResult);
                        }

                        string dalykoQuery = "SELECT id FROM dalykai WHERE pavadinimas = @dalykoPavadinimas";
                        int dalykoId;
                        using (MySqlCommand dalykoCmd = new MySqlCommand(dalykoQuery, conn))
                        {
                            dalykoCmd.Parameters.AddWithValue("@dalykoPavadinimas", dalykoPavadinimas);
                            object dalykoResult = dalykoCmd.ExecuteScalar();
                            if (dalykoResult == null)
                            {
                                MessageBox.Show("Dalykas nerastas.", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            dalykoId = Convert.ToInt32(dalykoResult);
                        }

                        // Priskirti dalyką grupei
                        string insertQuery = "INSERT INTO grupes_dalykai (grupes_id, dalyko_id) VALUES (@grupesId, @dalykoId)";
                        using (MySqlCommand insertCmd = new MySqlCommand(insertQuery, conn))
                        {
                            insertCmd.Parameters.AddWithValue("@grupesId", grupesId);
                            insertCmd.Parameters.AddWithValue("@dalykoId", dalykoId);
                            insertCmd.ExecuteNonQuery();

                            MessageBox.Show($"Dalykas '{dalykoPavadinimas}' sėkmingai priskirtas grupei '{grupesPavadinimas}'.", "Sėkmė", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (MySqlException ex) when (ex.Number == 1062) // Dublikato
                    {
                        MessageBox.Show("Šis dalykas jau yra priskirtas šiai grupei.", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Nepavyko priskirti dalyko: {ex.Message}", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
        }

        private void btnPridetiDestytoja_Click(object sender, EventArgs e)
        {
                string destytojasInput = txtDestytojas.Text.Trim();

                if (string.IsNullOrWhiteSpace(destytojasInput) || !destytojasInput.Contains(" "))
                {
                    MessageBox.Show("Prašome įvesti dėstytojo vardą ir pavardę (pvz., 'Jonas Jonaitis').", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string[] parts = destytojasInput.Split(' ');
                string vardas = parts[0];
                string pavarde = string.Join(" ", parts.Skip(1));

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();

                        // Įrašome dėstytoją į destytojai lentelę
                        string insertDestytojasQuery = "INSERT INTO destytojai (vardas, pavarde) VALUES (@vardas, @pavarde)";
                        using (MySqlCommand cmd = new MySqlCommand(insertDestytojasQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@vardas", vardas);
                            cmd.Parameters.AddWithValue("@pavarde", pavarde);
                            cmd.ExecuteNonQuery();
                        }

                        // Sukuriame prisijungimo duomenis (users lentelėje)
                        string userName = vardas.ToLower(); // Dėstytojo vardas kaip prisijungimo vardas
                        string password = pavarde.ToLower(); // Dėstytojo pavardė kaip slaptažodis

                        string insertUserQuery = "INSERT INTO users (vardas, slaptazodis, role) VALUES (@vardas, @slaptazodis, 'destytojas')";
                        using (MySqlCommand cmd = new MySqlCommand(insertUserQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@vardas", userName);
                            cmd.Parameters.AddWithValue("@slaptazodis", password);
                            cmd.ExecuteNonQuery();
                        }

                        // Gauti paskutinio įterpto dėstytojo ID
                        string getLastInsertedLecturerQuery = "SELECT id, CONCAT(vardas, ' ', pavarde) AS full_name FROM destytojai WHERE vardas = @vardas AND pavarde = @pavarde";
                        using (MySqlCommand cmd = new MySqlCommand(getLastInsertedLecturerQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@vardas", vardas);
                            cmd.Parameters.AddWithValue("@pavarde", pavarde);
                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    int id = Convert.ToInt32(reader["id"]);
                                    string fullName = reader["full_name"].ToString();

                                    cmbDestytojas.Items.Add(new KeyValuePair<int, string>(id, fullName));
                                    cmbDestytojas.DisplayMember = "Value";
                                    cmbDestytojas.ValueMember = "Key";

                                    cmbDestytojas.SelectedItem = new KeyValuePair<int, string>(id, fullName);
                                }
                            }
                        }

                        MessageBox.Show($"Dėstytojas '{vardas} {pavarde}' sėkmingai pridėtas.", "Sėkmė", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        txtDestytojas.Clear();
                    }
                    catch (MySqlException ex) when (ex.Number == 1062) // Dublikato klaida
                    {
                        MessageBox.Show("Toks dėstytojas jau egzistuoja.", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Nepavyko pridėti dėstytojo: {ex.Message}", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
        }

        private void btnSalintiDestytoja_Click(object sender, EventArgs e)
        {
                if (cmbDestytojas.SelectedItem == null)
                {
                    MessageBox.Show("Prašome pasirinkti dėstytoją.", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!(cmbDestytojas.SelectedItem is KeyValuePair<int, string> selectedLecturer))
                {
                    MessageBox.Show("Pasirinktas dėstytojas netinkamas.", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int lecturerId = selectedLecturer.Key;
                string lecturerName = selectedLecturer.Value; 

                DialogResult confirmation = MessageBox.Show(
                    $"Ar tikrai norite pašalinti dėstytoją '{lecturerName}'?",
                    "Patvirtinimas",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmation != DialogResult.Yes)
                    return;

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();

                        string deleteSubjectsQuery = "DELETE FROM destytojai_dalykai WHERE destytojo_id = @lecturerId";
                        using (MySqlCommand cmd = new MySqlCommand(deleteSubjectsQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@lecturerId", lecturerId);
                            cmd.ExecuteNonQuery();
                        }

                        string deleteLecturerQuery = "DELETE FROM destytojai WHERE id = @lecturerId";
                        using (MySqlCommand cmd = new MySqlCommand(deleteLecturerQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@lecturerId", lecturerId);
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show($"Dėstytojas '{lecturerName}' sėkmingai pašalintas.", "Sėkmė", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadLecturers(); 
                                cmbDestytojas.SelectedIndex = -1; 
                            }
                            else
                            {
                                MessageBox.Show("Nepavyko rasti dėstytojo duomenų bazėje.", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Nepavyko pašalinti dėstytojo: {ex.Message}", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
        }

        private void btnDestytojasD_Click(object sender, EventArgs e)
        {
                if (cmbDestytojas.SelectedItem == null)
                {
                    MessageBox.Show("Prašome pasirinkti dėstytoją.", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cmbDestytojasD.SelectedItem == null)
                {
                    MessageBox.Show("Prašome pasirinkti dalyką.", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    if (!(cmbDestytojas.SelectedItem is KeyValuePair<int, string> selectedLecturer))
                    {
                        MessageBox.Show("Neteisingas dėstytojo pasirinkimas.", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (!(cmbDestytojasD.SelectedItem is KeyValuePair<int, string> selectedSubject))
                    {
                        MessageBox.Show("Neteisingas dalyko pasirinkimas.", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    int lecturerId = selectedLecturer.Key;
                    int subjectId = selectedSubject.Key;

                    using (MySqlConnection conn = new MySqlConnection(connectionString))
                    {
                        conn.Open();

                        string query = "INSERT INTO destytojai_dalykai (destytojo_id, dalyko_id) VALUES (@destytojoId, @dalykoId)";
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@destytojoId", lecturerId);
                            cmd.Parameters.AddWithValue("@dalykoId", subjectId);
                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show($"Dėstytojas '{selectedLecturer.Value}' sėkmingai priskirtas dalykui '{selectedSubject.Value}'.", "Sėkmė", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (MySqlException ex) when (ex.Number == 1062)
                {
                    MessageBox.Show("Šis dėstytojas jau yra priskirtas šiam dalykui.", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Nepavyko priskirti dėstytojo dalykui: {ex.Message}", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }  

        }

        private void btnPridetiStudenta_Click(object sender, EventArgs e)
        {
                string studentasInput = txtStudentas.Text.Trim();

                if (string.IsNullOrWhiteSpace(studentasInput) || !studentasInput.Contains(" "))
                {
                    MessageBox.Show("Prašome įvesti studento vardą ir pavardę (pvz., 'Jonas Jonaitis').", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string[] parts = studentasInput.Split(' ');
                string vardas = parts[0];
                string pavarde = string.Join(" ", parts.Skip(1));

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();

                        string insertStudentasQuery = "INSERT INTO studentai (vardas, pavarde) VALUES (@vardas, @pavarde)";
                        using (MySqlCommand cmd = new MySqlCommand(insertStudentasQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@vardas", vardas);
                            cmd.Parameters.AddWithValue("@pavarde", pavarde);
                            cmd.ExecuteNonQuery();
                        }

                        string insertUserQuery = "INSERT INTO users (vardas, slaptazodis, role) VALUES (@vardas, @slaptazodis, 'studentas')";
                        using (MySqlCommand cmd = new MySqlCommand(insertUserQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@vardas", vardas);
                            cmd.Parameters.AddWithValue("@slaptazodis", pavarde); 
                            cmd.ExecuteNonQuery();
                        }

                        string getLastInsertedStudentQuery = "SELECT id, CONCAT(vardas, ' ', pavarde) AS full_name FROM studentai WHERE vardas = @vardas AND pavarde = @pavarde";
                        using (MySqlCommand cmd = new MySqlCommand(getLastInsertedStudentQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@vardas", vardas);
                            cmd.Parameters.AddWithValue("@pavarde", pavarde);
                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    int id = Convert.ToInt32(reader["id"]);
                                    string fullName = reader["full_name"].ToString();

                                    cmbStudentas.Items.Add(new KeyValuePair<int, string>(id, fullName));
                                    cmbStudentas.DisplayMember = "Value";
                                    cmbStudentas.ValueMember = "Key";

                                    cmbStudentas.SelectedItem = new KeyValuePair<int, string>(id, fullName);
                                }
                            }
                        }

                        MessageBox.Show($"Studentas '{vardas} {pavarde}' sėkmingai pridėtas.", "Sėkmė", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtStudentas.Clear();
                    }
                    catch (MySqlException ex) when (ex.Number == 1062) // Dublikato klaida
                    {
                        MessageBox.Show("Toks studentas arba vartotojas jau egzistuoja.", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Nepavyko pridėti studento: {ex.Message}", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
        }

        private void btnSalintiStudentas_Click(object sender, EventArgs e)
        {
                if (cmbStudentas.SelectedItem == null)
                {
                    MessageBox.Show("Prašome pasirinkti studentą.", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!(cmbStudentas.SelectedItem is KeyValuePair<int, string> selectedStudent))
                {
                    MessageBox.Show("Neteisingas studento pasirinkimas.", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int studentId = selectedStudent.Key;
                string studentName = selectedStudent.Value;

                DialogResult confirmation = MessageBox.Show(
                    $"Ar tikrai norite pašalinti studentą '{studentName}'?",
                    "Patvirtinimas",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirmation != DialogResult.Yes)
                    return;

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();

                        string deleteStudentQuery = "DELETE FROM studentai WHERE id = @studentId";
                        using (MySqlCommand cmd = new MySqlCommand(deleteStudentQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@studentId", studentId);
                            cmd.ExecuteNonQuery();
                        }

                        string deleteUserQuery = "DELETE FROM users WHERE vardas = @vardas";
                        using (MySqlCommand cmd = new MySqlCommand(deleteUserQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@vardas", studentName.Split(' ')[0]); 
                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show($"Studentas '{studentName}' sėkmingai pašalintas.", "Sėkmė", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        cmbStudentas.Items.Remove(selectedStudent);
                        cmbStudentas.SelectedIndex = -1;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Nepavyko pašalinti studento: {ex.Message}", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
        }

        private void btnStudentasG_Click(object sender, EventArgs e)
        {
                if (cmbStudentas.SelectedItem == null)
                {
                    MessageBox.Show("Prašome pasirinkti studentą.", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cmbStudentasG.SelectedItem == null)
                {
                    MessageBox.Show("Prašome pasirinkti grupę.", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!(cmbStudentas.SelectedItem is KeyValuePair<int, string> selectedStudent))
                {
                    MessageBox.Show("Neteisingas studento pasirinkimas.", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (!(cmbStudentasG.SelectedItem is KeyValuePair<int, string> selectedGroup))
                {
                    MessageBox.Show("Neteisingas grupės pasirinkimas.", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                int studentId = selectedStudent.Key;
                int groupId = selectedGroup.Key;

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();

                        string query = "INSERT INTO studentai_grupes (studento_id, grupes_id) VALUES (@studentId, @groupId)";
                        using (MySqlCommand cmd = new MySqlCommand(query, conn))
                        {
                            cmd.Parameters.AddWithValue("@studentId", studentId);
                            cmd.Parameters.AddWithValue("@groupId", groupId);
                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show($"Studentas '{selectedStudent.Value}' sėkmingai priskirtas grupei '{selectedGroup.Value}'.", "Sėkmė", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (MySqlException ex) when (ex.Number == 1062) // Dublikato klaida
                    {
                        MessageBox.Show("Šis studentas jau yra priskirtas šiai grupei.", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Nepavyko priskirti studento grupei: {ex.Message}", "Klaida", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
        }

        private void btnAtsijungti_Click(object sender, EventArgs e)
        {
            Form1 loginForm = new Form1();
            loginForm.Show();
            this.Close();
        }
    }
}
