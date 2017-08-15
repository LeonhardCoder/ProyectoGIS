<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Sucursales.aspx.cs" Inherits="ProyectoGIS.Sucursales" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="phbody" runat="server">
 


  <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="True">
            </asp:ScriptManager>
        <!-- Navigation -->
         <div >
                   <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                <ContentTemplate>
                    <div class="row">

                        

                        <div class="col-md-4">

                            <div >
                                <div class="panel-heading">
                                    <center><b><asp:Label ID="lblTitulo" runat="server" Text="Datos Sucursales"></asp:Label></b></center>
                                    <asp:Label ID="lblMensaje" CssClass="alert-danger" runat="server" Text=""></asp:Label>
                                </div>
                                <div style="background-color: #E6E6E6;">
                                    <asp:Label ID="lblResultado" runat="server" CssClass="alert-danger"></asp:Label>
                                    <div class="col-md-12">
                                        <div id="Div1" class="form-group col-md-12" runat="server">
                                            <asp:Label ID="Label1" runat="server" Text="Tipo"></asp:Label>
                                            <asp:DropDownList runat="server" ID="DropDownList1" Enabled="False">
                                                <asp:ListItem Text="Punto" Selected="True" />
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
                                            <asp:Label ID="lblaforo" runat="server" Text="aforo"></asp:Label>
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
                                            <asp:Button ID="btnInsertar" runat="server" Text="Insertar" CssClass="btn btn-success " OnClick="btnInsertar_Click"  />
                                            <asp:Button ID="btnEditar" runat="server" Text="Editar" CssClass="btn btn-primary " Enabled="False" OnClick="btnEditar_Click" />
                                            <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-primary " Enabled="False" OnClick="btnEliminar_Click" />
                                            <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar " CssClass="btn btn-default " Enabled="False" OnClick="btnLimpiar_Click" />
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <div class="col-md-8">
                          
                            <div style="width: 100%; height: 100%;">
                                <gmaps:GMap ID="GMap1" runat="server" enableServerEvents="true" serverEventsType="AspNetPostBack"
                                    OnMarkerClick="GMap1_MarkerClick"
                                    OnClick="GMap1_Click"
                                    Height="-100" Width="-100" Language="es" />
                            </div>
                            <br />
                        </div>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="DropDownList1" EventName="SelectedIndexChanged" />
                     <asp:PostBackTrigger ControlID="GMap1" />
                </Triggers>
            </asp:UpdatePanel>
            </div>


</asp:Content>
