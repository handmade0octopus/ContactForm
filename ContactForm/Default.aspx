<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server" >
    <div class="container">
        <fieldset>
            <h3 class="welcome">Welcome to my contact page</h3>
        </fieldset>
        <fieldset>
            Full Name <br />
            <fieldset>
                <asp:TextBox class="textboxarea" ID="txtName" MaxLength="40" runat="server" required="true" />
            </fieldset>
        </fieldset>
        
        <fieldset>
                Email address <br />
            <fieldset>
                <asp:TextBox class="textboxarea" type="email" ID="txtEmail" MaxLength="40" runat="server" required="true" />
            </fieldset>
        </fieldset>

        <fieldset>
                Phone number <br />
            <fieldset>
                <asp:TextBox class="textboxarea" input="txtNumber" ID="txtNumber" MaxLength="11" runat="server" required="true" />
            </fieldset>
        </fieldset>

        <fieldset>
                Message <br />
            <fieldset>
                <asp:TextBox class="box" TextMode="MultiLine" MaxLength="255" ID="txtMessage" runat="server" />
            </fieldset>
        </fieldset>
        <fieldset>
            <fieldset>
                <asp:CheckBox ID="checkGdpr" runat="server"/> * I agree to GDPR rules and T&C
            </fieldset>
        </fieldset>

        <fieldset>
            <fieldset>
                <asp:CheckBox ID="checkNewsletter" runat="server"/> I wish to receive updates from you
            </fieldset>
        </fieldset>

         <fieldset>
        <asp:Button class="submitbutton" ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"/>
        </fieldset>

        <h4 class="error">
                <asp:Label Text="" ID="lblAfterSubmit" runat="server" Visible="true" />
                <asp:RequiredFieldValidator runat="server" ID="rfvName" ControlToValidate="txtName" ErrorMessage="Name required <br />" display="Dynamic"/> 
                <asp:RegularExpressionValidator runat="server" ID="revName" ValidationExpression="^([a-zA-Z-0-4]*?)\s+([a-zA-Z-0-4]*){1,40}$"  ControlToValidate="txtName" ErrorMessage="Full name required <br />" display="Dynamic"/> 
                <asp:RequiredFieldValidator runat="server" ID="rfvEmail"  ControlToValidate="txtEmail" ErrorMessage="Email address required <br />" display="Dynamic"/> 
                <asp:RegularExpressionValidator runat="server" ID="revEmail" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"  ControlToValidate="txtEmail" ErrorMessage="Valid email address required <br />" display="Dynamic"/> 
                <asp:RequiredFieldValidator runat="server" ID="rfvNumber" ControlToValidate="txtNumber" ErrorMessage="Phone number required <br />" display="Dynamic"/> 
                <asp:RegularExpressionValidator runat="server" ID="revNumber" ValidationExpression="^[+]?[0-9]{4,11}$"  ControlToValidate="txtNumber" ErrorMessage="Valid phone number required <br />" display="Dynamic"/> 
                <asp:CustomValidator runat="server" ID="rfvGdpr" onservervalidate="rfvGdpr_ServerValidate" ErrorMessage="GDPR and T&C required <br />" display="Dynamic"/> 
                <asp:RegularExpressionValidator runat="server" ID="revMessage" ValidationExpression="[^\s]{0,255}"  ControlToValidate="txtMessage" ErrorMessage="Message too long <br />" display="Dynamic"/> 
        </h4>
        
    </div>

    <br />
    <div>
    </div>
       <asp:GridView class="gridview" GridLines="None" ID="gvContact" runat="server" AutoGenerateColumns="false" OnRowDeleting="gvContact_RowDeleting" OnRowEditing="gvContact_RowEditing" OnRowUpdating="gvContact_RowUpdating" OnRowCancelingEdit="gvContact_RowCancelingEdit">
           <Columns>
               <asp:TemplateField>
                   <ItemTemplate>
                       <asp:HiddenField ID="hdnId" Value='<%# DataBinder.Eval(Container.DataItem, "id") %>' runat="server" />
                   </ItemTemplate>
               </asp:TemplateField>
               <asp:BoundField DataField="name" HeaderText="Name"/>
               <asp:BoundField DataField="email" HeaderText="Email"/>
               <asp:BoundField DataField="number" HeaderText="Number"/>
               <asp:BoundField DataField="gdpr" HeaderText="GDPR Checked"/>
               <asp:BoundField DataField="newsletter" HeaderText="Newsletter checked"/>
               <asp:BoundField DataField="message" HeaderText="Message"/>
               <asp:CommandField ShowEditButton="true" />
               <asp:CommandField ShowDeleteButton="true" />

           </Columns>
       </asp:GridView>
    </asp:Content>

