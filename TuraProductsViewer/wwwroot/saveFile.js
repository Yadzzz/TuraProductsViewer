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