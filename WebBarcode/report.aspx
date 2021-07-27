<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="report.aspx.cs" Inherits="WebBarcode.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>  
    <form id="form1" runat="server">  
        <div class="container py-4">  
            <h5 class="text-uppercase text-center">How to export gridview data in word,excel and Pdf format using asp.net</h5>  
            <div class="card">  
                <div class="card-header bg-primary text-white">  
                    <h5 class="card-title text-uppercase">Employees List</h5>  
                </div>  
                <div class="card-body">  
                    <asp:Button ID="btnExportToWord" CssClass="btnMargin btn btn-outline-primary rounded-0" runat="server" Text="ExportToWord" OnClick="btnExportToWord_Click" />  
                    <asp:Button ID="btnExportToExcel" CssClass="btnMargin btn btn-outline-primary rounded-0" runat="server" Text="ExportToExcel" OnClick="btnExportToExcel_Click" />  
                    <asp:Button ID="btnExportToPDF" CssClass="btnMargin btn btn-outline-primary rounded-0" runat="server" Text="ExportToPDF" OnClick="btnExportToPDF_Click" />  
                    <asp:GridView ID="EmployeeGridViewList" CssClass="table table-bordered" runat="server"></asp:GridView>  
                </div>  
            </div>  
        </div>  
    </form>  
</body> 
</html>
