function loadAudio(src){
	return new Promise(async (res,rej)=>{
		let blob = await (await fetch(src)).blob();
		res(URL.createObjectURL(blob));
	});
}



document.addEventListener('DOMContentLoaded', async ()=>{
    console.log('%c😺 МРЯ!', 'color: #f678ff; font-size: 24px; font-weight: bold;');
	// Посхалка с футой
	if(Math.random() <= 0.05){
		document.querySelector('#tuta').innerText = 'Тута есть фута :3';
		// document.querySelector('#coming').innerText = 'Cumming soon...';
	}

	// Лизь
	console.log('%cНажми F6, чтобы получить лизь', 'color: #ff00ff;');
    window.addEventListener("keydown", (e) => {
        if (e.code === "F6") {
            window.location.href = "https://ryzh.pl/lick?autoLick=nya";
        }
    });

	// Кликабельный ральзей
	const ralsei = document.querySelector('#Ralsei');
	const ralseiButton = document.querySelector('#RalseiButton');
	let animationInProgress = false;

        const squish = await loadAudio('/assets/squish.ogg');
        const boop = await loadAudio('/assets/boop.mp3');
	const explode = await loadAudio('/assets/deltarune-explosion.mp3');

	let clicks = 0, clickAllowed = true;

	ralseiButton.addEventListener('click', e=>{
		if(clicks >= 250){
			clicks = 0;
			clickAllowed = false;
	
			let oldSrc = ralsei.src;
			ralsei.src = '/assets/explosion-deltarune.gif';

			ralsei.addEventListener('load', ()=>{
				let aud = new Audio(explode);
				aud.onended = ()=>{
					clickAllowed = true;
					ralsei.src = oldSrc;
					ralsei.style.display = '';
				};

				aud.play();

				setTimeout(()=>{
					ralsei.style.display = 'none';
				}, 700);
			}, { once: true });
		}else{
			clicks++;
		}

		if(!clickAllowed) return;

		ralsei.classList.remove('squish-animation');
		void ralsei.offsetWidth;
		ralsei.classList.add('squish-animation');

		const clientRect = ralsei.getBoundingClientRect();
		if(e.x - clientRect.left >= 57 && e.x - clientRect.left <= 78 && e.y - clientRect.top >= 57 && e.y - clientRect.top <= 78){
			new Audio(boop).play();
		}else{
			new Audio(squish).play();
		}
	});

	ralsei.ondragstart = e=>{
		e.preventDefault();
		return false;
	};

	// Бэкграунд для таблички
	// Я эту фигню пилил на протяжении всей пары
	const container = document.querySelector('.sick-cool-background');
	const heart = document.querySelector('.heart');
	const cells = document.querySelectorAll('.navigation_div');

	function moveBackToCell(cell) {
		const containerRect = container.getBoundingClientRect();
		const cellRect = cell.getBoundingClientRect();

		heart.style.top = (cellRect.top - containerRect.top) + 'px';
		heart.style.left = (cellRect.left - containerRect.left) + 'px';
		heart.style.width = (cellRect.width) + 'px';
		heart.style.height = (cellRect.height) + 'px';
	}

	// Фон есть
	cells.forEach(cell => {
		cell.addEventListener('mouseenter', () => {
			moveBackToCell(cell);
			heart.style.opacity = '1';
			cell.classList.add('navigation_hover');
		});
	});

	// Фона нет

	cells.forEach(cell => {
		cell.addEventListener('mouseleave', () => {
			heart.style.opacity = '0';
			cell.classList.remove('navigation_hover');
		});
	});

	console.log("Все скриптики запущены!");
});

