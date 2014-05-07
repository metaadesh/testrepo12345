<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DealerListGoogleMap.aspx.cs"
    Inherits="METAOPTION.UI.DealerListGoogleMap" Title="HeadstartVMS::Purchased From/Sold To List"
    MasterPageFile="~/UI/MasterPageFullScreen.Master" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?key=<%= GoogleMapKey %>&sensor=false">
    </script>
    <script type="text/javascript">
        var geocoder;
        var map;
        var markers = [];
        var count_marker = 0;
        var infoWindow;

//        var DealerList = new Array(
//         new Array('Arun gupta', 'New Delhi India', 11, 14, ' 03 - 12 - 2013'),
//         new Array('Tarun talyan', 'Mumbai India', 19, 14, '23 - 02 - 2014'),
//         new Array('Abhinav Mishra', 'Noida India', 26, 2, '06 - 04 - 2013'),
//         new Array('Manly Seman', 'NewYork USA', 90, 44, '08 - 09 - 2013'),
//         new Array('Maroubra Drien', 'Ottawa Canada', 24, 32, '25 - 01 - 2014'),
//         new Array('Alix Rz', 'Stockholm Sweden', 46, 32, '12 - 05 - 2010'),
//         new Array('Marian Seman', 'Atlanta Georgia USA', 90, 44, '01 - 09 - 2013'),
//         new Array('Stone Gold', 'Les Vegaus Nevada USA', 74, 32, '15 - 01 - 2014'),
//         new Array('Justin Black', 'Birminghum United Kingdom', 103, 32, '19 - 01 - 2010')
//             );

        var DealerList = <%=DealerList %>




        function codeAddress() {
            //alert("WORKING");
            clearLocations();
            var address;
            var PinImage; var j = 0;
            geocoder = new google.maps.Geocoder();
            //var address = document.getElementById("txtcity").value;
            for (var i = 0; i < DealerList.length; i++) {
                address = DealerList[i][1];

                geocoder.geocode({ address: address }, function (results, status) {

                    if (status == google.maps.GeocoderStatus.OK) {

                        //  if (results[0].geometry.location_type == "ROOFTOP")
                        //          map.setZoom(7);
                        //       else
                        //          map.setZoom(6);
                        // if(address === DealerList[i][1]){
                        //  map.setCenter(results[0].geometry.location);

                        PinImage = setimage(DealerList[j][2]);

                        var CurrentMarker = new google.maps.Marker({
                            position: results[0].geometry.location,
                            animation: google.maps.Animation.DROP,
                            icon: PinImage,
                            map: map
                        });

                        var newinfoWindow = new google.maps.InfoWindow({
                            content: "<div style='width: 350px; height:150px; padding:10px;'>" + "<b>" + DealerList[j][0] + "</b><br>" + results[0].formatted_address + "<br><br><b>Number of Unit Sold to: </b>" + DealerList[j][2] + "<br><b>Number Purchased From:</b>" + DealerList[j][3] + "<br><b>Last Transaction Date: </b>" + DealerList[j][4] + "</div>"   /*results[i].formatted_address */
                        });

                        markers.push(CurrentMarker);
                        CommonPinEvent(CurrentMarker, newinfoWindow);
                    }
                    else {
                    //sleep(100);
                    alert(address);
                        //alert('No Location found for this Search');
                    }
                    j++;
                });
            }
        }

        function sleep(delay) {
        var start = new Date().getTime();
        while (new Date().getTime() < start + delay);
      }

        function initialize() {

            var latlng = new google.maps.LatLng(40.790278, -73.959722); //provide location by latitude and longitude
            var mapOptions = {
                zoom: 3,
                center: latlng,
                mapTypeId: google.maps.MapTypeId.TERRAIN
            }
            map = new google.maps.Map(document.getElementById('googleMap'), mapOptions);

            var returnMarker = new google.maps.Marker({
                position: latlng,
                animation: google.maps.Animation.DROP,
                icon: "http://maps.google.com/mapfiles/ms/icons/blue-dot.png",
                map: map
            });
            markers.push(returnMarker);

            infoWindow = new google.maps.InfoWindow({
                content: "<div style='width: 250px; height: 50px; padding:10px;'>" + " Manhatten NY USA" + "</div>"
            });
            CommonPinEvent(returnMarker, infoWindow);

            codeAddress();
        }

        //  CURRENTLY NOT IN USE, THIS CODE IS COPIED IN PARENT FUNCTION

        //function addMarker(location) {                             
        //  
        //    // iterator = DealerList[0][1];      Add a marker to the map and push to the array.
        //    var PinImage;
        //    for (var i = 0; i < DealerList.length; i++) {
        //        PinImage = setimage(DealerList[i][2]);

        //        var CurrentMarker = new google.maps.Marker({
        //            position: location.geometry.location,
        //            animation: google.maps.Animation.DROP,
        //            //icon: "http://maps.google.com/mapfiles/ms/icons/yellow-dot.png",
        //            icon: PinImage,
        //            map: map
        //        });

        //        var newinfoWindow = new google.maps.InfoWindow({
        //            content: "<div style='width: 300px; height:100px; padding:10px;'>" + "<b>" + DealerList[i][0] + "</b><br>" + location.formatted_address + "<br><br><b>Number of Unit Sold to: </b>" + DealerList[i][2] + "<br><b>Number Purchased From:</b>" + DealerList[i][3] + "<br><b>Last Transaction Date: </b>" + DealerList[i][4] + "</div>"   /*results[i].formatted_address */
        //        });

        //        markers.push(CurrentMarker);
        //    }
        //      return CurrentMarker;
        //    }



        function CommonPinEvent(marker, infoWindow) {

            google.maps.event.addListener(marker, 'mouseover', function () {
                infoWindow.open(map, marker);
            });
            google.maps.event.addListener(marker, 'mouseout', function () {
                infoWindow.close();
            });

            google.maps.event.addListener(marker, 'click', function () {
                map.setZoom(10);
                map.setCenter(marker.getPosition());
            });
        }


        function clearLocations() {
            infoWindow.close();
            for (var i = 0; i < markers.length; i++) {
                markers[i].setMap(null);
            }
            markers.length = 0;
        }



        function setimage(Units_Count) {
            var url;
            if (Units_Count > 0 && Units_Count <= 24) {
                count_marker++;
                url = 'http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=' + count_marker + '|FFFF00|000000';
                return url;
            }
            else if (Units_Count > 24 && Units_Count < 75) {
                count_marker++;
                url = 'http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=' + count_marker + '|00CC66|000000';
                return url;
            }
            else if (Units_Count >= 75) {
                count_marker++;
                url = 'http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=' + count_marker + '|FF0000|000000';
                return url;
            }
        }
    </script>
    <script type="text/javascript">
        google.maps.event.addDomListener(window, 'load', initialize);
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div id="googleMap" style="width: 100%; height: 500px;">
        </div>
        <table>
            <tr>
                <td>
                    City
                </td>
                <td>
                    <asp:TextBox ID="txtcity" runat="server" Width="209px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    State
                </td>
                <td>
                    <asp:TextBox ID="txtstate" runat="server" Width="209px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Country
                </td>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server" Width="208px"></asp:TextBox>
                </td>
                <td>
                    <%--<asp:Button ID="SearchButton" runat="server" Text="Search Location" OnClientClick="codeAddress()" />--%>
                    <input type="button" onclick="codeAddress()" value="Search" />
                </td>
                <%--OnClick="searchLocations()"--%>
            </tr>
        </table>
    </div>
</asp:Content>
