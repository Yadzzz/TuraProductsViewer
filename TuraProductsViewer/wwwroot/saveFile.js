//function saveFile(file, Content) {
//    var link = document.createElement('a');
//    link.download = name;
//    link.href = "data:text/plain;charset=utf-8," + encodeURIComponent(Content)
//    document.body.appendChild(link);
//    link.click();
//    document.body.removeChild(link);
//}

function saveAsFile(filename, bytesBase64) {
    var link = document.createElement('a');
    link.download = filename;
    link.href = "data:application/octet-stream;base64," + bytesBase64;
    document.body.appendChild(link); // Needed for Firefox
    link.click();
    document.body.removeChild(link);
}

function saveAsPdfFile(filename, bytesBase64) {
    var link = document.createElement('a');
    link.download = filename;
    link.href = "data:application/pdf;base64," + bytesBase64;
    document.body.appendChild(link); // Needed for Firefox
    link.click();
    document.body.removeChild(link);
}

function saveByteArray(reportName, byte) {
    var blob = new Blob([byte], { type: "application/pdf" });
    var link = document.createElement('a');
    link.href = window.URL.createObjectURL(blob);
    var fileName = reportName;
    link.download = fileName;
    link.click();
};

function generatePDF() {
    var pdf = new jsPDF({
        orientation: 'p',
        unit: 'mm',
        format: 'a4',
        putOnlyUsedFonts: true
    });
    pdf.text("Generate a PDF with JavaScript", 20, 20);
    pdf.text("published on APITemplate.io", 20, 30);
    pdf.addPage();
    pdf.text(20, 20, 'The second page');
    pdf.save('jsPDF_2Pages.pdf');
};

function generatePDFFF() {
    
};

