function play(video, control) {
    var player = document.querySelector('#video');
    if (player.paused) {
        player.play();
        control.textContent = 'Pause';
    }
    else {
        player.pause();
        control.textContent = 'Play';
    }
    
}

function stop(video) {
    var player = document.querySelector('#video');
    
    player.currentTime = 0;
    player.pause();
}

function update(player) {
    var duration = player.duration;    // Durée totale
    var time     = player.currentTime; // Temps écoulé
    document.querySelector('#progressTime').textContent = formatTime(time) +" / " +formatTime(duration);
}


function formatTime(duration) {
    var hours = Math.floor(time / 3600);
    var mins  = Math.floor((time % 3600) / 60);
    var secs  = Math.floor(time % 60);
	
    if (secs < 10) {
        secs = "0" + secs;
    }
	
    if (hours) {
        if (mins < 10) {
            mins = "0" + mins;
        }
		
        return hours + ":" + mins + ":" + secs; // hh:mm:ss
    } 
    else {
        return mins + ":" + secs; // mm:ss
    }
}

function formatTime(time) {
    var hours = Math.floor(time / 3600);
    var mins  = Math.floor((time % 3600) / 60);
    var secs  = Math.floor(time % 60);
	
    if (secs < 10) {
        secs = "0" + secs;
    }
	
    if (hours) {
        if (mins < 10) {
            mins = "0" + mins;
        }
		
        return hours + ":" + mins + ":" + secs; // hh:mm:ss
    } 
    else {
        return mins + ":" + secs; // mm:ss
    }
}