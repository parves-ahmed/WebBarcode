<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeBehind="DetailsOfPerson.aspx.cs" Inherits="WebBarcode.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script>
</head>
    <script type="text/javascript">
    $(function () {
        $("[id*=btnExportpdfhtml]").click(function () {
            $("[id*=hfGridHtml1]").val($("#Grid").html());
        });
    });
    </script>
<body>
    <form id="form1" runat="server">
         <div class="container py-4">  
            <h5 class="text-uppercase text-center">How to export gridview data in word,excel and Pdf format using asp.net</h5>  
            <div class="card">  
                <div class="card-header bg-primary text-white">  
                    <h5 class="card-title text-uppercase">Employees List</h5>  
                </div>  
                <div class="card-body" id="Grid">  
                   <%-- <asp:Button ID="btnExportToWord" CssClass="btnMargin btn btn-outline-primary rounded-0" runat="server" Text="Export To Word" OnClick="btnExportToWord_Click" />  
                    <asp:Button ID="btnExportToExcel" CssClass="btnMargin btn btn-outline-primary rounded-0" runat="server" Text="Export To Excel" OnClick="btnExportToExcel_Click" /> --%>   
                    <asp:GridView ID="PersonGridViewList" CssClass="table table-bordered" runat="server"></asp:GridView>  
                   
                </div>  
            </div>  
        </div> 
          <asp:HiddenField ID="hfGridHtml1" runat="server" />
          <asp:Button ID="btnExportpdfhtml" CssClass="btnMargin btn btn-outline-primary rounded-0" runat="server" Text="Export To PDF From HTML" OnClick="ExportToPDFHtml" />
    </form>
</body>
</html>
