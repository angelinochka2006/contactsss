namespace contactsss
{
    public partial class  ContactForm : Form
    {
        private ContactManager contactManager;
        private TextBox nameTextBox;
        private TextBox phoneNumberTextBox;
        private Button addContactButton;
        private Button removeContactButton;
        private TextBox searchTextBox;
        private Button searchButton;
        private ListBox contactsListBox;
        public ContactForm()
        {
            this.Text = "���������� ����������";
            this.Width = 500;
            this.Height = 400;
            nameTextBox = new TextBox
            {
                Location = new System.Drawing.Point(10, 10),
                Width = 150,

            };
            phoneNumberTextBox = new TextBox
            {
                Location = new System.Drawing.Point(170, 10),
                Width = 150,

            };
            addContactButton = new Button
            {
                Location = new System.Drawing.Point(10, 40),
                Text = "��������",
                Width = 100
            };
            addContactButton.Click += AddContactButton_Click;
            removeContactButton = new Button
            {
                Location = new System.Drawing.Point(120, 40),
                Text = "�������",
                Width = 100
            };
            removeContactButton.Click += RemoveContactButton_Click;
            searchTextBox = new TextBox
            {
                Location = new System.Drawing.Point(10, 70),
                Width = 200,

            };
            searchButton = new Button
            {
                Location = new System.Drawing.Point(220, 70),
                Text = "������",
                Width = 80
            };
            searchButton.Click += SearchButton_Click;
            contactsListBox = new ListBox
            {
                Location = new System.Drawing.Point(10, 100),
                Width = 450,
                Height = 200
            };
            this.Controls.Add(nameTextBox);
            this.Controls.Add(phoneNumberTextBox);
            this.Controls.Add(addContactButton);
            this.Controls.Add(removeContactButton);
            this.Controls.Add(searchTextBox);
            this.Controls.Add(searchButton);
            this.Controls.Add(contactsListBox);
            contactManager = new ContactManager();
            UpdateContactsList();
        }
        private void UpdateContactsList()
        {
            contactsListBox.Items.Clear();
            foreach (var contact in contactManager.Contacts)
            {
                contactsListBox.Items.Add($"{contact.Name} - {contact.PhoneNumber}");
            }
        }
        private void AddContactButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(nameTextBox.Text) ||
            string.IsNullOrEmpty(phoneNumberTextBox.Text))
            {
                MessageBox.Show("��������� ��� ����!");
                return;
            }
            Contact newContact = new Contact(nameTextBox.Text, phoneNumberTextBox.Text);
            try
            {
                contactManager.AddContact(newContact);
                nameTextBox.Clear();
                phoneNumberTextBox.Clear();
                UpdateContactsList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void RemoveContactButton_Click(object sender, EventArgs e)
        {
            if (contactsListBox.SelectedIndex == -1)
            {
                MessageBox.Show("�������� ������� ��� ��������!");
                return;
            }
            string selectedItem = contactsListBox.SelectedItem.ToString();
            string[] parts = selectedItem.Split(new[] { '-' }, StringSplitOptions.None);
            if (parts.Length >= 2)
            {
                string name = parts[0].Trim();
                string phoneNumber = parts[1].Trim();
                var contactToRemove = contactManager.Contacts.Find(c => c.Name == name &&
                c.PhoneNumber == phoneNumber);
                if (contactToRemove != null)
                {
                    try
                    {
                        contactManager.RemoveContact(contactToRemove);
                        UpdateContactsList();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }
        private void SearchButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(searchTextBox.Text))
            {
                UpdateContactsList();
                return;
            }
            var searchResults = contactManager.SearchContacts(searchTextBox.Text);
            contactsListBox.Items.Clear();
            foreach (var contact in searchResults)
            {
                contactsListBox.Items.Add($"{contact.Name} - {contact.PhoneNumber}");
            }
        }

    }
}
