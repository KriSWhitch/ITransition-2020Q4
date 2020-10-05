let wordsArray = process.argv.slice(2), outArray = [];
if (wordsArray.length === 0) return;
wordsArray.forEach(e => {
    let tmpArray = [];
    wordsArray.forEach(el => {
        if (el.split('').sort().join('') === e.split('').sort().join('')) tmpArray.push(el);
    });
    if ( outArray.length < tmpArray.length ) outArray = tmpArray;
});
if (outArray.length === 1) {
    console.log(outArray[0]);
    return;
} 
outArray.forEach(e => console.log(e));
