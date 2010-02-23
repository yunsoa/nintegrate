<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LiveMaps.ascx.cs" Inherits="Provider_LiveMaps" %>

<script type="text/javascript">   
    <!--   
      var healthProviders;
      
      function pageLoad() {   
        // Load the Virtual Earth map.   
        var map = new VEMap('map');               
        map.LoadMap();       
                   
        // Invoke the web method.   
        if (!healthProviders)
            PageMethods.GetProviderList(providerSearchValues[0],
                                        providerSearchValues[1],
                                        providerSearchValues[2],
                                        providerSearchValues[3],
                                        AddProvidersToMap, onMethodFailed);        
        else
            AddProvidersToMap(healthProviders);
                   
        // Add the providers
        function AddProvidersToMap(providers) {   
            var shp;
            var arrProviders = new Array;
            var curLatLong;
            healthProviders = providers;
            
            map.DeleteAllShapes();
            
            for (var i = 0; i < providers.length; i++)
            {
                curLatLong = new VELatLong(providers[i].Latitude, 
                                            providers[i].Longitude);
                                            
                arrProviders.push(curLatLong);
                
                shp = new VEShape(VEShapeType.Pushpin, curLatLong);
                shp.SetTitle(providers[i].Name);
                shp.SetDescription(providers[i].Name + ', ' + providers[i].Organization);

                map.AddShape(shp);
            }    
    
            map.SetMapView(arrProviders);
        }
  
        function onMethodFailed() {   
          alert('Providers map could not be loaded. Please contact your system administrator.');   
        }   
      }     
    //-->   
</script>

<div id="map" style="position: relative; width: 100%"/>


