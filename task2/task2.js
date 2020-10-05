sha3_256 = require('js-sha3').sha3_256;
const fs = require('fs');
var path = require('path');
let arr = [];
function fromDir(startPath,filter){

  //console.log('Starting from dir '+startPath+'/');

  if (!fs.existsSync(startPath)){
      console.log("no dir ",startPath);
      return;
  }

  var files=fs.readdirSync(startPath);
  for(var i=0;i<files.length;i++){
      var filename=path.join(startPath,files[i]);
      var stat = fs.lstatSync(filename);
      if (stat.isDirectory()){
          continue; //recurse
      }
      else if (filename.indexOf(filter)>=0) {
          arr.push(filename);
      };
  };
};
fromDir('./','');
arr.forEach(e => {
  const data = fs.readFileSync(e, 'utf-8')
  console.log(`${e} ${sha3_256(data)}`);
});



