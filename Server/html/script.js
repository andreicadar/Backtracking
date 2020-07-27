$("#detalii :input").change(function() {
   $("#detalii").data("changed",true);
});

if ($("#detalii").data("changed")) {
   alert(2);

}