// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const UPriceText = price => {
    $("#MoviePriceLabel").text("$" + price);
}
const UReleaseDate = () => {
    var date = new Date();
    var day = date.getDate();
    var month = date.getMonth() + 1; //La funcion obtiene el indice del mes, por eso se agreaga 1.
    var year = date.getFullYear();
    //Parseo dia y mes para cumplir el formato yyyy/mm/dd
    day = day < 9 ? "0" + day : day;
    month = month < 9 ? "0" + month : month;
    $("#MovieReleaseDate").val(year + "-" + month + "-" + day);
}
UReleaseDate();
UPriceText($("#MoviePriceRange").val());

$("#MoviePriceRange").change(function () {
    UPriceText($("#MoviePriceRange").val());
});