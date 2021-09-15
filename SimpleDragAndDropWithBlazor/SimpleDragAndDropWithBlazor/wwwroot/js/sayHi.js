export function sayHi(name) {
    alert(`hello ${name}!`);
}

export function setupDropzone(dropzone, time, ref) {

    dropzone.ondragover = function () {
        this.className = 'dropzone dragover';
        return false;
    }

    dropzone.ondragleave = function () {
        this.className = 'dropzone';
        return false;
    }

    dropzone.ondrop = function (e) {
        e.preventDefault();
        this.className = 'dropzone drop';
        //upload(e.dataTransfer.files);
        //ref.invokeMethodAsync('UploadCaller', e.dataTransfer);
        var json = JSON.stringify(e);
        console.log(json);
    }

    //var upload = function (files) {
    //    ref.invokeMethodAsync('UploadCaller', files);
    //    //for (var x = 0; x < files.length; x = x + 1) {
    //    //    //ref.invokeMethodAsync('UploadCaller', files[x].name);
    //    //}
    //}

}