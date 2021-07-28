<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeBehind="report.aspx.cs" Inherits="WebBarcode.WebForm1" %>

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
        $("[id*=btnExport]").click(function () {
            $("[id*=hfGridHtml]").val($("#Grid").html());
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
                <div class="card-body">
                    <%--<asp:Button ID="Button1" CssClass="btnMargin btn btn-outline-primary rounded-0" runat="server" Text="Export To PDF From HTML" OnClick="ExportToPDF" />--%>
                    <div id="Grid">
                        <table class="table table-bordered">
                            <thead>
                                <tr>
                                    <th style="background-color: #B8DBFD; border: 1px solid #ccc">Chesis No.</th>
                                    <th style="background-color: #B8DBFD; border: 1px solid #ccc">Color</th>
                                    <th style="background-color: #B8DBFD; border: 1px solid #ccc">Type</th>
                                    <th style="background-color: #B8DBFD; border: 1px solid #ccc">CC</th>
                                    <th style="background-color: #B8DBFD; border: 1px solid #ccc">Other</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td style="width: 120px; border: 1px solid #ccc">ch06789</td>
                                    <td style="width: 150px; border: 1px solid #ccc">Red</td>
                                    <td style="width: 120px; border: 1px solid #ccc">Motor Cycle</td>
                                    <td style="width: 150px; border: 1px solid #ccc">100</td>
                                    <td style="width: 120px; border: 1px solid #ccc">
                                        <asp:HyperLink ID="Hyperlink1" runat="server" NavigateUrl="DetailsOfPerson.aspx?Country=Bangladesh">SubForms</asp:HyperLink>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 120px; border: 1px solid #ccc">ch012345</td>
                                    <td style="width: 150px; border: 1px solid #ccc">Black</td>
                                    <td style="width: 120px; border: 1px solid #ccc">Motor Cycle</td>
                                    <td style="width: 150px; border: 1px solid #ccc">150</td>
                                    <td style="width: 120px; border: 1px solid #ccc">
                                        <asp:HyperLink ID="NavLink1" runat="server" NavigateUrl="DetailsOfPerson.aspx?Country=UK">SubForms</asp:HyperLink>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
            <br />
            <asp:HiddenField ID="hfGridHtml" runat="server" />
            <%--<asp:Button ID="btnExport" runat="server" Text="Export To PDF" OnClick="ExportToPDF" />--%>
            <asp:Button ID="btnExport" CssClass="btnMargin btn btn-outline-primary rounded-0" runat="server" Text="Export To PDF From HTML" OnClick="ExportToPDF" />
            <asp:Button ID="btnExportToExcel" CssClass="btnMargin btn btn-outline-primary rounded-0" runat="server" Text="Export To Excel" OnClick="btnExportToExcel_Click" />  
        </div>
        

        <div class="container py-4">  
            <h5 class="text-uppercase text-center">How to export gridview data in word,excel and Pdf format using asp.net</h5>  
            <div class="card">  
                <div class="card-header bg-primary text-white">  
                    <h5 class="card-title text-uppercase">Employees List</h5>  
                </div>  
                <div class="card-body">  
                    <asp:Button ID="btnExportToWord" CssClass="btnMargin btn btn-outline-primary rounded-0" runat="server" Text="Export To Word" OnClick="btnExportToWord_Click" />  
                    
                    <asp:Button ID="btnExportToPDF" CssClass="btnMargin btn btn-outline-primary rounded-0" runat="server" Text="Export To PDF" OnClick="btnExportToPDF_Click" />  
                    <asp:GridView ID="EmployeeGridViewList" CssClass="table table-bordered" runat="server"></asp:GridView>  
                </div>  
            </div>  
        </div>
  </form> 
    
  
</body> 
</html>
