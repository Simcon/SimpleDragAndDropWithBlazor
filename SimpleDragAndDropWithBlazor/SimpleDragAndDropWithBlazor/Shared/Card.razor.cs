using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using SimpleDragAndDropWithBlazor.Models;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SimpleDragAndDropWithBlazor.Shared
{
    public partial class Card : ComponentBase, IAsyncDisposable
    {
        [Parameter] public CardModel CardModel { get; set; }
        [Inject] IJSRuntime JS { get; set; }

        private ElementReference dropZoneElement;
        private ElementReference inputFileContainer;
        private ElementReference _linkGrabberElement;

        private IJSObjectReference _module;
        private IJSObjectReference _module2;
        private IJSObjectReference _dropZoneInstance;

        private string src;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                // Load the JS file
                _module = await JS.InvokeAsync<IJSObjectReference>("import", "./js/dropZone.js").AsTask();
                _module2 = await JS.InvokeAsync<IJSObjectReference>("import", "./js/linkGrabber.js").AsTask();

                // Initialize the drop zone
                _dropZoneInstance = await _module.InvokeAsync<IJSObjectReference>("initializeFileDropZone", dropZoneElement, inputFileContainer);

                await _module2.InvokeAsync<IJSObjectReference>("plop", _linkGrabberElement);
            }
        }

        // Called when a new file is uploaded
        async Task OnChange(InputFileChangeEventArgs e)
        {
            var selectedFiles = e.GetMultipleFiles();
            foreach (var file in selectedFiles)
            {
                using var stream = file.OpenReadStream();
                using var ms = new MemoryStream();
                await stream.CopyToAsync(ms);
                src = "data:" + file.ContentType + ";base64," + Convert.ToBase64String(ms.ToArray());
            }
        }

        // Unregister the drop zone events
        public async ValueTask DisposeAsync()
        {
            if (_dropZoneInstance != null)
            {
                await _dropZoneInstance.InvokeVoidAsync("dispose");
                await _dropZoneInstance.DisposeAsync();
            }

            if (_module != null)
            {
                await _module.DisposeAsync();
            }
        }
    }
}
