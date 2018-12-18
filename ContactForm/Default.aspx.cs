using System;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

/**
 * Contact form made with MySql and WebForms
 **/

public partial class _Default : System.Web.UI.Page {

    // Config string and object of MySql connection
    string strConnString;
    MySqlConnection connection;

    protected void Page_Load(object sender, EventArgs e) {
        if (!this.IsPostBack) {
            Connect();
        }
    }

    protected MySqlConnection Connect()
    {
        strConnString = ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString;
        return connection = new MySqlConnection(strConnString);
    }

    // On button click
    protected void btnSubmit_Click(object sender, EventArgs e) {
        if(Page.IsValid) {
            lblAfterSubmit.Text = string.Empty;
            TextBox txName = txtName;
            TextBox txEmail = txtEmail;
            TextBox txNumber = txtNumber;
            int gdpr;
            
            if (checkGdpr.Checked)
            {
                gdpr = 1;
            }
            else gdpr = 0;

            int newsletter;
            if (checkNewsletter.Checked)
            {
                newsletter = 1;
            }
            else newsletter = 0;
            TextBox txMessage = txtMessage;

            using (Connect())
            {
                try
                {
                    connection.Open();
                    if (txName.Text == "l33t h4ck")
                    {
                        BindDataToGridView();
                    }
                    else
                    {
                        string sql = string.Format("INSERT INTO contact (name, email, number, gdpr, newsletter, message) values ('{0}', '{1}', '{2}', {3}, {4}, '{5}')", txName.Text, txEmail.Text, txNumber.Text, gdpr, newsletter, txMessage.Text);
                        MySqlCommand command = new MySqlCommand(sql, connection);
                        command.ExecuteNonQuery();
                        lblSubmitted.Visible=true;
                    }
                    
                }
                catch (MySqlException ex)
                {
                    lblAfterSubmit.Text = "Error: " + ex.Message;
                }
                finally
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
        } else
        {
        }
    }

    // Binds data from MySql to secret GridView
    protected void BindDataToGridView() {
        using (Connect())
        {
            try
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("SELECT id, name, email, number, gdpr, newsletter, message FROM contact ORDER BY id", connection);
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter(command);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet);
                if(dataSet.Tables[0].Rows.Count > 0)
                {
                    gvContact.DataSource = dataSet;
                    gvContact.DataBind();
                }
            }
            catch (MySqlException ex)
            {
                lblAfterSubmit.Text = "Error: " + ex.Message;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }
    }


    // Custom checker checks if required checkmark is checked
    protected void rfvGdpr_ServerValidate(object source, ServerValidateEventArgs args) {
        args.IsValid = checkGdpr.Checked;
    }
    
    // Control for the debuging mode
    protected void gvContact_RowEditing(object sender, GridViewEditEventArgs e)
    {
        lblAfterSubmit.Text = string.Empty;
        gvContact.EditIndex = e.NewEditIndex;
        BindDataToGridView();
    }

    protected void gvContact_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        lblAfterSubmit.Text = string.Empty;
        GridViewRow gvRow = (GridViewRow)gvContact.Rows[e.RowIndex];
        HiddenField hdId = (HiddenField)gvRow.FindControl("hdnId");

        using (Connect())
        {
            try
            {
                connection.Open();
                string sql = string.Format("DELETE FROM contact WHERE id={0}", hdId.Value);
                MySqlCommand command = new MySqlCommand(sql, connection);
                command.ExecuteNonQuery();
                gvContact.EditIndex = -1;
                BindDataToGridView();
            }
            catch (MySqlException ex)
            {
                lblAfterSubmit.Text = "Error: " + ex.Message;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }
    }

    protected void gvContact_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        lblAfterSubmit.Text = string.Empty;
        GridViewRow gvRow = (GridViewRow)gvContact.Rows[e.RowIndex];
        HiddenField hdId = (HiddenField)gvRow.FindControl("hdnId");
        TextBox txName = (TextBox)gvRow.Cells[1].Controls[0];
        TextBox txEmail = (TextBox)gvRow.Cells[2].Controls[0];
        TextBox txNumber = (TextBox)gvRow.Cells[3].Controls[0];
        TextBox txGdpr = (TextBox)gvRow.Cells[4].Controls[0];
        TextBox txNewsletter = (TextBox)gvRow.Cells[5].Controls[0];

        int gdpr;
        if (txGdpr.Text.ToString() == (string) "1")
        {
            gdpr = 1;
        }
        else gdpr = 0;

        int newsletter;
        if (txNewsletter.Text.ToString() == (string) "1")
        {
            newsletter = 1;
        }
        else newsletter = 0;

        TextBox txMessage = (TextBox)gvRow.Cells[6].Controls[0];

        using (Connect())
        {
            try
            {
                connection.Open();
                string sql = string.Format("UPDATE Contact set name='{0}', email='{1}', number='{2}', gdpr={3}, newsletter={4}, message='{5}' WHERE id={6}", txName.Text, txEmail.Text, txNumber.Text, gdpr, newsletter, txMessage.Text, hdId.Value);
                MySqlCommand command = new MySqlCommand(sql, connection);
                command.ExecuteNonQuery();
                gvContact.EditIndex = -1;
                BindDataToGridView();

            }
            catch (MySqlException ex)
            {
                lblAfterSubmit.Text = "Error: " + ex.Message;
            }
            finally
            {
                connection.Close();
                connection.Dispose();
            }
        }
    }

    protected void gvContact_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvContact.EditIndex = -1;
        BindDataToGridView();
    }
}