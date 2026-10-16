const iceUrl = 'https://radio.xn--l1aij1c.xn--p1ai/status-json.xsl';
const streamUrl = 'http://xn--l1aij1c.xn--p1ai:1256/main.ogg'
let radioActive = false;

async function StartRadio(){
    const player = document.getElementById("silliradio");
    const nowPlaying = document.getElementById("nowPlaying");
    const playButton = document.getElementById("playbutton");
    radioActive = true;

    player.play();
    playButton.innerText = (' ❚❚ ');
    playButton.href = ('javascript:StopRadio()')

    console.log("Я сказало стартуем!");
    // console.log(player);

    const volumeSlider = document.getElementById('volume');
    console.log(volumeSlider);
    volumeSlider.addEventListener('oninput', e=>{
        player.volume = e.triger.value / 100;
        console.log(e.target.value);
    });

    //Тут тооооочно не будет проблем :3
    while (radioActive){
        UpdateNowPlaying(nowPlaying);
        await sleep(5000);
    }

}

async function StopRadio(){
    const player = document.getElementById("silliradio");
    const nowPlaying = document.getElementById("nowPlaying");
    const playButton = document.getElementById("playbutton");
    radioActive = false;

    player.pause();
    playButton.innerText = (' ▶︎ ');
    playButton.href = ('javascript:StartRadio()')
    nowPlaying.innerText = ('Тык на плей')

    console.log("Я сказало стопоримся!")
}

// Я так долго тупил с этим сраным фетчем, а оказалось нужно было всего лишь async поставить -_-
async function UpdateNowPlaying(nowPlaying){
        const response = await fetch(iceUrl);

        const result = await response.json();
        let source = result.icestats.source

        if (Array.isArray(source)){
            source.forEach(x => {
                if(x.listenurl == streamUrl) source = x;
            })
        }
        var hasArtist = source.artist == undefined ? false : true;

        nowPlaying.innerText = ('Сейчас играет: ' + (hasArtist ? source.artist + ' - ' : '') + source.title);
}

function sleep(ms) {
    return new Promise(resolve => setTimeout(resolve, ms));
}

