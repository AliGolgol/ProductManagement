$(function () {
    $('#chartInv').ShowChart({
        //url: "@@ @Url.RouteUrl("DefaultApi", new { httproute = "", controller = "Invoices" })",
           //url: "@Url.Action("GetInvoiceItem", "Home")",
           url: "../api/Invoices",
        legendPosition: "right",
    seriesType: "column",
    text: "فاکتور",
    categoryAxisLabelsRotation: 45,
    font: '16px irans',
    theme: 'Silver'
});

$('#chartBuy').ShowChart({
    //url: "@Url.RouteUrl("DefaultApi", new { httproute = "", controller = "products" })",
    //url: "@Url.Action("GetBuySlipItem", "Home")",
    legendPosition: "right",
    seriesType: "column",
    text: "رسید",
    categoryAxisLabelsRotation: -45,
    font: '16px irans',
    theme: 'Silver'
});

function changeChartType(type) {
    var chartInv = $("#chartInv").data("kendoChart");
    chartInv.options.series[0].type = type;
    chartInv.refresh();

    var chartBuy = $("#chartBuy").data("kendoChart");
    chartBuy.options.series[0].type = type;
    chartBuy.refresh();
}


kendo.pdf.defineFont({
    /*"Verdana": "/fonts/Verdana.ttf", // this is a URL
    "Verdana|Bold": "/fonts/Verdana_Bold.ttf",
    "Verdana|Bold|Italic": "/fonts/Verdana_Bold_Italic.ttf",
    "Verdana|Italic": "/fonts/Verdana_Italic.ttf"*/
    "Iranian Sans": "/fonts/irsans.ttf"
});

function doExportInv() {
    var chart = $("#chartInv").data("kendoChart");

    chart.saveAsPDF(); // it needs version 2014.3.1119 +
    // or
    chart.exportImage().done(function (data) {
        kendo.saveAs({
            dataURI: chart.imageDataURL(),
            fileName: "chartInv.png"
        });
    });
}

function doExportBuy() {
    var chart = $("#chartBuy").data("kendoChart");

    chart.saveAsPDF(); // it needs version 2014.3.1119 +
    // or
    chart.exportImage().done(function (data) {
        kendo.saveAs({
            dataURI: chart.imageDataURL(),
            fileName: "chartBuy.png"
        });
    });
}
});