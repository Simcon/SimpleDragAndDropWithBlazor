export function plop(ele) {

	var LinkGrabber = {
		textarea: null,

		/* Textarea Management */

		attach_ta: function () {
			if (LinkGrabber.textarea != null) return;

			var textarea = LinkGrabber.textarea = document.createElement("textarea");
			textarea.setAttribute("style", "position: fixed; width: 100%; margin: 0; top: 0; bottom: 0; right: 0; left: 0; z-index: 99999999");
			textarea.style.opacity = "0.000000000000000001";

			//var body = document.getElementsByTagName("body")[0];
			ele.appendChild(textarea);

			textarea.oninput = LinkGrabber.evt_got_link;
		},

		detach_ta: function () {
			if (LinkGrabber.textarea == null) return;
			var textarea = LinkGrabber.textarea;

			textarea.parentNode.removeChild(textarea);
			LinkGrabber.textarea = null;
		},

		/* Event Handlers */

		evt_drag_over: function () {
			LinkGrabber.attach_ta(); //Create TA overlay
		},

		evt_got_link: function () {
			var link = LinkGrabber.textarea.value;

			alert(link);

			LinkGrabber.detach_ta();
		},

		evt_drag_out: function (e) {
			if (e.target == LinkGrabber.textarea) LinkGrabber.detach_ta();
		},

		/* Start/Stop */

		start: function () {
			ele.addEventListener("dragover", LinkGrabber.evt_drag_over, false);
			ele.addEventListener("dragenter", LinkGrabber.evt_drag_over, false);

			ele.addEventListener("mouseup", LinkGrabber.evt_drag_out, false);
			ele.addEventListener("dragleave", LinkGrabber.evt_drag_out, false);
		},

		//stop: function () {
		//	document.removeEventListener("dragover", LinkGrabber.evt_drag_over);
		//	document.removeEventListener("dragenter", LinkGrabber.evt_drag_over);

		//	document.removeEventListener("mouseup", LinkGrabber.evt_drag_out);
		//	document.removeEventListener("dragleave", LinkGrabber.evt_drag_out);

		//	LinkGrabber.detach_ta();
		//}
	};

	LinkGrabber.start();

}