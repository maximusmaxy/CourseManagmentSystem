﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CmsLibrary;

namespace CMS
{
    public partial class StudentForm : Form
    {
        public StudentForm()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //validation
            if (!Validation.Many(
                txtFirstName.ValidateWord(),
                txtLastName.ValidateWord(),
                txtStreet1.ValidateEmpty(),
                txtStreet2.ValidateEmpty(),
                txtSuburb.ValidateWord(),
                cmbState,
                txtPostCode.ValidateNumeric(),
                txtEmail.ValidateEmail(),
                cmbCountryOfOrigin,
                pnlGender
                ))
            {
                return;
            }
            //location
            Location location = new Location()
            {
                AddressStreet1 = txtStreet1.Text,
                AddressStreet2 = txtStreet2.Text,
                AddressSuburb = txtSuburb.Text,
                AddressState = cmbState.Text,
                AddressPostCode = txtPostCode.Int()
            };
            if (!location.Add())
            {
                return;
            }
            //student
            Student student = new Student()
            {
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                LocationId = location.Id,
                DateOfBirth = dtpDateOfBirth.Value,
                Email = txtEmail.Text,
                CountryOfOrigin = cmbCountryOfOrigin.Text,
                Gender = Forms.RadioValue(pnlGender, typeof(GenderType)),
                Disability = chkDisability.Checked,
                DisabilityDescription = chkDisability.Checked ? txtDisabilityDescription.Text : null
            };
            if (!student.Add())
            {
                return;
            }
            //success!
            MessageBox.Show($"Student id: {student.Id} added successfully.");
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //validation
            if (!Validation.Numeric(txtId))
                return;
            //student
            Student student = new Student(txtId.Int());
            if (student.Search())
            {
                txtFirstName.Text = student.FirstName;
                txtLastName.Text = student.LastName;
                dtpDateOfBirth.Value = student.DateOfBirth;
                txtEmail.Text = student.Email;
                cmbCountryOfOrigin.Text = student.CountryOfOrigin;
                Forms.CheckRadio(pnlGender, typeof(GenderType), student.Gender);
                chkDisability.Checked = student.Disability;
                txtDisabilityDescription.Text = student.DisabilityDescription;
                //location
                Location location = new Location(student.LocationId);
                if (location.Search())
                {
                    txtStreet1.Text = location.AddressStreet1;
                    txtStreet2.Text = location.AddressStreet2;
                    txtSuburb.Text = location.AddressState;
                    cmbState.Text = location.AddressState;
                    txtPostCode.Text = location.AddressPostCode.ToString();
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            //validation
            if (!Validation.Many(
                txtId.ValidateNumeric(),
                txtFirstName.ValidateWord(),
                txtLastName.ValidateWord(),
                txtStreet1.ValidateEmpty(),
                txtStreet2.ValidateEmpty(),
                txtSuburb.ValidateWord(),
                cmbState,
                txtPostCode.ValidateNumeric(),
                txtEmail.ValidateEmail(),
                cmbCountryOfOrigin,
                pnlGender
                ))
            {
                return;
            }
            //location
            Location location = new Location()
            {
                AddressStreet1 = txtStreet1.Text,
                AddressStreet2 = txtStreet2.Text,
                AddressSuburb = txtSuburb.Text,
                AddressState = cmbState.Text,
                AddressPostCode = txtPostCode.Int()
            };
            if (!location.Update())
            {
                return;
            }
            //student
            Student student = new Student()
            {
                Id = txtId.Int(),
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                LocationId = location.Id,
                DateOfBirth = dtpDateOfBirth.Value,
                Email = txtEmail.Text,
                CountryOfOrigin = cmbCountryOfOrigin.Text,
                Gender = Forms.RadioValue(pnlGender, typeof(GenderType)),
                Disability = chkDisability.Checked,
                DisabilityDescription = chkDisability.Checked ? txtDisabilityDescription.Text : null
            };
            if (!student.Update())
            {
                return;
            }
            MessageBox.Show($"Student id: {student.Id} successfully deleted.");
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //validation
            if (!Validation.Numeric(txtId))
            {
                return;
            }
            //student
            Student student = new Student(txtId.Int());
            if (!student.Delete())
            {
                return;
            }
            MessageBox.Show($"Student id: {student.Id} successfully deleted.");
        }

        private void chkDisability_CheckedChanged(object sender, EventArgs e)
        {
            txtDisabilityDescription.Enabled = chkDisability.Checked;
        }

        private void btnViewAll_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Not Implemented.");
        }
        //comment
    }
}
