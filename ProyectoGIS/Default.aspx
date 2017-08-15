<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ProyectoGIS.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">

    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <title>Ingreso de Datos proyecto GIS</title>
    <!-- Latest compiled and minified CSS -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous" />

    <!-- Optional theme -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap-theme.min.css" integrity="sha384-rHyoN1iRsVXV4nD0JutlnGaslCJuC7uwjduW9SVrLvRYooPp2bWYgmgJQIXwl/Sp" crossorigin="anonymous" />
</head>
<body  style="background-color: #F7F2E0;">
    <nav class="navbar navbar-inverse">
        <div class="container-fluid">
            <div class="navbar-header">
                <a class="navbar-brand" href="#">Home</a>
            </div>
            <ul class="nav navbar-nav">
                <li class="active"><a href="Default.aspx">Ingreso de Datos<span class="sr-only">(current)</a></li>
                <li><a href="Visualizador.aspx">OPEN LAYER VIEWER - WMS</a></li>
            </ul>
        </div>
    </nav>
    <div class="container">
        <form id="form1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="True">
            </asp:ScriptManager>
             <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
            <div class="row">
              
                <div class="col-md-8">
                    <div style="width: 100%; height: 100%;">
                        <gmaps:GMap ID="GMap1" runat="server" enableServerEvents="true" serverEventsType="AspNetPostBack"
                            OnMarkerClick="GMap1_MarkerClick"
                            OnClick="GMap1_Click"
                            Height="-100" Width="-100" Language="es" 
                            
                            />
                    </div>
                    <br />
                </div>

                 <div class="col-md-4">
                   
                            <div class="panel panel-success">
                                <div class="panel-heading">
                                    <center><b><asp:Label ID="lblTitulo" runat="server" Text="Datos Sucursales"></asp:Label></b></center>
                                    <asp:Label ID="lblMensaje" CssClass="alert-danger" runat="server" Text=""></asp:Label>
                                </div>
                                <div class="panel-body" style="background-color: #E6E6E6;">
                                    <asp:Label ID="lblResultado" runat="server" CssClass="alert-danger"></asp:Label>
                                    <div class="col-md-12">
                                        <div id="Div1" class="form-group col-md-12" runat="server">
                                            <asp:Label ID="Label1" runat="server" Text="Tipo"></asp:Label>
                                            <asp:DropDownList runat="server" ID="DropDownList1" Enabled="False">
                                                <asp:ListItem Text="Punto"  Selected="True"/>
                                                <asp:ListItem Text="Linea" />
                                                 <asp:ListItem Text="Poligon" />
                                            </asp:DropDownList>        
                                        </div>
                                        <div id="nombre" class="form-group col-md-12" runat="server">
                                            <asp:Label ID="lblNombre" runat="server" Text="Nombre sucursal"></asp:Label>
                                            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" AutoPostBack="True"></asp:TextBox>
                                            
                                        </div>
                                        <div id="Direccion" class="form-group col-md-12" runat="server">
                                            <asp:Label ID="lbldireccion" runat="server" Text="Dirección"></asp:Label>
                                            <asp:TextBox ID="txtdireccion" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div id="aforo" class="form-group col-md-12" runat="server">
                                            <asp:Label ID="lblaforo" runat="server" Text="aforo" ></asp:Label>
                                            <asp:TextBox ID="txtaforo" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <asp:Label ID="lblLongitud" runat="server" Text="Longitud 1"></asp:Label>
                                            <asp:TextBox ID="txtLongitud" runat="server" CssClass="form-control" Enabled="False" AutoPostBack="True"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <asp:Label ID="lblLatitud" runat="server" Text="Latitud 1"></asp:Label>
                                            <asp:TextBox ID="txtLatitud" runat="server" CssClass="form-control" Enabled="False"></asp:TextBox>
                                        
                                    </div>
                                </div>
                                <div class="panel-footer">
                                    <br />
                                    <div class="form-group">
                                        <asp:Button ID="btnInsertar" runat="server" Text="Insertar" CssClass="btn btn-success " OnClick="btnInsertar_Click" Enabled="False" />
                                        <asp:Button ID="btnEditar" runat="server" Text="Editar" CssClass="btn btn-info " Enabled="False" OnClick="btnEditar_Click" />
                                        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger " Enabled="False" OnClick="btnEliminar_Click" />
                                        <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar " CssClass="btn btn-default " Enabled="False" OnClick="btnLimpiar_Click" />
                                    </div>
                                </div>
                            </div>
                     
                </div>
            </div>
               </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="DropDownList1"  EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
        </form>
    </div>
    <!-- /.container -->
    <br />
    <br />
    <footer id="footer">
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <div class="alert alert-info" role="alert">
                        Copyright &copy; <%: DateTime.Now.Year %>
                        
                    </div>
                </div>
            </div>
        </div>

    </footer>

</body>
</html>
