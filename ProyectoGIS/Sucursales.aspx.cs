
using Npgsql;
using Subgurim.Controles;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ProyectoGIS
{
	public partial class Sucursales : System.Web.UI.Page
	{
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        private IList<Punto> listaPunto = new List<Punto>();

        GLatLng ubicacion = new GLatLng(-1.45, -78.52);

        protected void Page_Load(object sender, EventArgs e)
        {
            bool[] habilitar = { true, false, false, false, false, false, false, false };
            VisibleTexto(habilitar);
            GMap1.resetMarkers();
            GMap1.resetScreenOverlays();
            GMap1.resetInfoWindows();
            if ((List<Punto>)Session["listaPunto"] != null)
            {
                listaPunto = (List<Punto>)Session["listaPunto"];

            }

            GMap1.enableDragging = false; // mover el mapa con el mouse
            GMap1.Language = "es"; //lenguaje

            //Establecemos alto y ancho en px
            GMap1.Height = 570;
            //GMap1.Width = 760;
            GMap1.enableHookMouseWheelToZoom = true; // permitir zoom con la rueda del mouse



            //GLatLng ubicacion = new GLatLng(-77.5, -2);
            GMapType.GTypes maptype = GMapType.GTypes.Normal;

            GMap1.setCenter(ubicacion, 7, maptype);
            List<GLatLng> puntos = new List<GLatLng>();
            if (DropDownList1.SelectedValue == "Linea" && listaPunto.Count > 0)
            {

                foreach (var p in listaPunto)
                {
                    GLatLng latLng = new GLatLng(Convert.ToDouble(p.latitud), Convert.ToDouble(p.longitud));
                    puntos.Add(latLng);
                }
                //puntos.Add(ubicacion);

            }

            GPolyline linea = new GPolyline(puntos, "FF0000", 2);
            GMap1.Add(linea);


            BuildMap();

        }

        private void BuildMap()
        {
            dt = BD.GetTable();
            Double lat, lng;

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    lat = Convert.ToDouble(row["latitud"].ToString().Replace('.',','));
                    lng = Convert.ToDouble(row["longitud"].ToString().Replace('.', ','));
                    GLatLng latLng = new GLatLng(lat, lng);



                    GIcon icon = new GIcon();
                    icon.image = "https://www.google.es/maps/vt/icon/name=icons/spotlight/spotlight-poi-medium.png&scale=2?scale=1";
                    icon.shadow = "http://labs.google.com/ridefinder/images/mm_20_shadow.png";
                   
                    GMarkerOptions mOpts = new GMarkerOptions();
                    mOpts.clickable = true;
                    mOpts.icon = icon;
                    GMarker marker = new GMarker(latLng, mOpts);
                    //GMap1.Add(marker);
                    
                    GInfoWindow window = new GInfoWindow(marker,
                                                         string.Format(
                                                             @"<span style='color:blue;'>{0} </span><br />
                                                               <span ><b>Latitutd:</b></span> {1} <br /> 
                                                               <span ><b>Longitud:</b>:</span> {2} <br />
                                                               <b>aForo:</b> {3}",
                                                                row["name_branch"].ToString(),
                                                                row["latitud"].ToString(),
                                                                row["longitud"].ToString(),
                                                                row["aforo"].ToString()
                                                         ),
                                                         true);
                    GMap1.Add(window);
                }
            }

            KeyDragZoom keyDragZoom = new KeyDragZoom();
            keyDragZoom.key = KeyDragZoom.HotKeyEnum.ctrl;
            keyDragZoom.boxStyle = "{border: '4px solid #FFFF00'}";
            keyDragZoom.VeilStyle = "{backgroundColor: 'black', opacity: 0.2, cursor: 'crosshair'}";

            GMap1.Add(keyDragZoom);

            GCustomCursor customCursor = new GCustomCursor(cursor.crosshair, cursor.text);
            GMap1.Add(customCursor);

            GMap1.Add(new GControl(GControl.preBuilt.LargeMapControl));

            GMap1.Add(new GListener(GMap1.GMap_Id, GListener.Event.zoomend,
             string.Format(@"
                   function(oldLevel, newLevel)
                   {{
                      if ((newLevel > 7) || (newLevel < 4))
                      {{
                          var ev = new serverEvent('AdvancedZoom', {0});
                          ev.addArg(newLevel);
                          ev.send();
                      }}
                   }}
                   ", GMap1.GMap_Id)));


        }

        protected string GMap1_Click(object s, GAjaxServerEventArgs e)
        {
            string respuesta = string.Empty;
            Punto pun = new Punto();
            pun.longitud = e.point.lng.ToString();
            pun.latitud = e.point.lat.ToString();
            listaPunto.Add(pun);

            Session["listaPunto"] = listaPunto;

            if (DropDownList1.SelectedValue == "Punto")
            {
                GIcon icon = new GIcon();
                icon.image = "https://www.google.es/maps/vt/icon/name=icons/spotlight/spotlight-poi-medium.png&scale=2?scale=1";
                icon.shadow = "http://labs.google.com/ridefinder/images/mm_20_shadow.png";
                GMarkerOptions mOpts = new GMarkerOptions();
                mOpts.clickable = true;
                mOpts.icon = icon;
                GMarker marker = new GMarker(e.point, mOpts);
                GInfoWindow window = new GInfoWindow(marker,
                                                     string.Format(
                                                         @"<b>Longitud y Latitud del Punto</b><br />SW = {0}<br/>NE = {1} <br /> LatLogn{2}",
                                                         e.bounds.getSouthWest(),
                                                         e.bounds.getNorthEast(),
                                                         e.point
                                                     ),
                                                     true);
                txtNombre.Focus();
                txtLatitud.Text = e.point.lat.ToString();
                txtLongitud.Text = e.point.lng.ToString();
                txtNombre.Text = string.Empty;

                btnInsertar.Enabled = true;
                btnLimpiar.Enabled = true;
                respuesta = window.ToString(e.map);
            }
            if (DropDownList1.SelectedValue == "Linea" && listaPunto.Count > 1)
            {
                GMarker marker = new GMarker(e.point);
                GInfoWindow window = new GInfoWindow(marker,
                                                     string.Format(
                                                         @"<b>Longitud y Latitud del Punto</b><br />SW = {0}<br/>NE = {1} <br /> LatLogn{2}",
                                                         e.bounds.getSouthWest(),
                                                         e.bounds.getNorthEast(),
                                                         e.point
                                                     ),
                                                     true);
                respuesta = window.ToString(e.map);
            }
            return respuesta;
        }

        protected string GMap1_MarkerClick(object s, GAjaxServerEventArgs e)
        {

            dt = BD.GetTableOne(e.point.lat.ToString(), e.point.lng.ToString());
            if (dt.Rows.Count > 0)
            {
                btnEditar.Enabled = true;
                btnEliminar.Enabled = true;
                btnLimpiar.Enabled = true;

                foreach (DataRow row in dt.Rows)
                {
                    txtLatitud.Text = e.point.lat.ToString();
                    txtLongitud.Text = e.point.lng.ToString();
                    txtNombre.Text = row["name_branch"].ToString();
                    txtdireccion.Text = row["addres_branch"].ToString();
                    txtaforo.Text = row["aforo"].ToString();
                   
                }
            }
            return string.Empty;
            //return string.Format("alert('MarkerClick: {0} - {2} - {1}'); ", e.point, DateTime.Now, e.map);
        }


        protected void btnInsertar_Click(object sender, EventArgs e)
        {
            if (txtNombre.Text == "")
            {
                lblMensaje.Text = "Debe Ingresar el nombre de la sucursal";
            }
            else
            {
                if (txtaforo.Text == "")
                {
                    lblMensaje.Text = "Debe Ingresar el numero de aforo de la sucursal";
                }else { 
                lblMensaje.Text = "";
                BD.InsertInTable(txtNombre.Text, txtdireccion.Text, int.Parse(txtaforo.Text), txtLatitud.Text, txtLongitud.Text);
                BuildMap();
                limpiar();
                }
            }

        }



        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            limpiar();

        }
        public void limpiar()
        {
            txtLatitud.Text = string.Empty;
            txtLongitud.Text = string.Empty;
            txtNombre.Text = string.Empty;
            txtdireccion.Text = string.Empty;
            txtaforo.Text = string.Empty;

            btnEditar.Enabled = false;
            btnEliminar.Enabled = false;
            btnInsertar.Enabled = false;
            btnLimpiar.Enabled = false;
            lblMensaje.Text = string.Empty;
        }
        protected void btnEditar_Click(object sender, EventArgs e)
        {
            BD.UpdateInTable(txtNombre.Text, txtdireccion.Text, int.Parse(txtaforo.Text), txtLatitud.Text, txtLongitud.Text);
            BuildMap();
            limpiar();
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            BD.DeleteInTable(txtLatitud.Text, txtLongitud.Text);
            BuildMap();
            limpiar();
        }



        protected void btnVisualizador_Click(object sender, EventArgs e)
        {
            Server.Transfer("Visualizador.aspx");
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //lblResultado.Text = "Entra";
            if (DropDownList1.SelectedValue == "Punto")
            {
                listaPunto.Clear();
                lblTitulo.Text = "Sucursales  Cercanas";
                bool[] habilitar = { true, false, false, false, false, false, false, false };
                VisibleTexto(habilitar);
            }
            if (DropDownList1.SelectedValue == "Linea")
            {
                listaPunto.Clear();
                lblTitulo.Text = "Rutas ";
                bool[] habilitar = { false, true, false, true, true, true, true, true };
                VisibleTexto(habilitar);
            }
            if (DropDownList1.SelectedValue == "Poligono")
            {
                listaPunto.Clear();
                lblTitulo.Text = "Zonas asignadas";
                bool[] habilitar = { false, false, true, true, true, true, true, true };
                VisibleTexto(habilitar);
            }
        }

        public void VisibleTexto(bool[] habilitar)
        {
            /*punto.Visible = habilitar[0];
            txtNombre.Visible = habilitar[0];
            lblNombre.Visible = habilitar[0];
            linea.Visible = habilitar[1];
            txtRuta.Visible = habilitar[1];
            lblRuta.Visible = habilitar[1];
            poligono.Visible = habilitar[2];
            txtZona.Visible = habilitar[2];
            lblZona.Visible = habilitar[2];
           /* txtLatitud2.Visible = habilitar[3];
            lblLatitud2.Visible = habilitar[3];
            txtLongitud2.Visible = habilitar[3];
            lblLongitud2.Visible = habilitar[3];
            txtLatitud3.Visible = habilitar[4];
            lblLatitud3.Visible = habilitar[4];
            txtLongitud3.Visible = habilitar[4];
            lblLongitud3.Visible = habilitar[4];
            txtLatitud4.Visible = habilitar[5];
            lblLatitud4.Visible = habilitar[5];
            txtLongitud4.Visible = habilitar[5];
            lblLongitud4.Visible = habilitar[5];
            txtLatitud5.Visible = habilitar[6];
            lblLatitud5.Visible = habilitar[6];
            txtLongitud5.Visible = habilitar[6];
            lblLongitud5.Visible = habilitar[6];
            txtLatitud6.Visible = habilitar[7];
            lblLatitud6.Visible = habilitar[7];
            txtLongitud6.Visible = habilitar[7];
            lblLongitud6.Visible = habilitar[7];*/
        }

    }
}
