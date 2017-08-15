<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ProyectoGIS.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <!-- The above 3 meta tags *must* come first in the head; any other head content must come *after* these tags -->
    <meta name="description" content="">
    <title>Visualizador Open Layer - WMS </title>
    <meta name="description" content="">

    <link rel="stylesheet" href="http://openlayers.org/en/v3.2.0/css/ol.css" type="text/css">
    <script src="http://openlayers.org/en/v3.2.0/build/ol.js" type="text/javascript"></script>
    <link rel="stylesheet" href="ol3-layerswitcher-master/src/ol3-layerswitcher.css" type="text/css" />
    <script src="ol3-layerswitcher-master/src/ol3-layerswitcher.js"></script>
    <style>
        #map {
            cursor: default;
            height: 600px;
            margin: 0 !important;
            padding: 0 !important;
            width: 100%;
         
        }
        titulo{
		padding:5px;
		text-align:center;
		color: #3f51b5;
		
	  }
	  
	  body{
		background-color:silver;
	  }
    </style>
    <!-- Latest compiled and minified CSS -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous" />

    <!-- Optional theme -->
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap-theme.min.css" integrity="sha384-rHyoN1iRsVXV4nD0JutlnGaslCJuC7uwjduW9SVrLvRYooPp2bWYgmgJQIXwl/Sp" crossorigin="anonymous" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="phbody" runat="server">

     <div class="container">
        <div class="row">
            <h1 class="titulo">Visualizador de mapas</h1>
            <div>
            </div>

            <div style="clear: left"></div>
            <hr>
            <div class="col-md-12">
                <div id="map"></div>
                <script>
                    var map = new ol.Map({
                        layers:
                            [new ol.layer.Group({
                                'title': ' Mapa Base',
                                layers: [

                                    new ol.layer.Tile({
                                        title: 'OSM',
                                        type: 'base',
                                        visible: true,
                                        source: new ol.source.OSM()
                                    }),
                                    new ol.layer.Tile({
                                        title: 'Google Mapa',
                                        type: 'base',
                                        visible: false,
                                        source: new ol.source.TileImage({ url: 'http://maps.google.com/maps/vt?pb=!1m5!1m4!1i{z}!2i{x}!3i{y}!4i256!2m3!1e0!2sm!3i375060738!3m9!2spl!3sUS!5e18!12m1!1e47!12m3!1e37!2m1!1ssmartmaps!4e0' })
                                    }),
                                    new ol.layer.Tile({
                                        title: 'Google Satelite',
                                        type: 'base',
                                        visible: false,
                                        source: new ol.source.TileImage({ url: 'http://khm0.googleapis.com/kh?v=717&hl=pl&&x={x}&y={y}&z={z}' })
                                    })

                                ]
                            }),
                            new ol.layer.Group({
                                title: 'CapasDeber',
                                layers: [

                                   new ol.layer.Tile({
                                        title: 'locales',
                                        source: new ol.source.TileWMS({
                                            url: 'http://localhost:8070/geoserver/uisrael_9noC/wms',
                                            params: { 'LAYERS': 'uisrael_9noC:sucursal' },
                                            serverType: 'geoserver'
                                        }),
                                        visible: false
                                    })



                                ]
                            })
                            ],
                        target: 'map',
                        view: new ol.View({
                            center: ol.proj.transform([-78.45, -0.96], 'EPSG:4326', 'EPSG:3857'),
                            zoom: 7
                        })
                    });

                    var layerSwitcher = new ol.control.LayerSwitcher({
                        tipLabel: 'Leyenda'
                    });
                    map.addControl(layerSwitcher);
                    layerSwitcher.showPanel();

                </script>
            </div>
        </div>
    </div>
   
</asp:Content>
