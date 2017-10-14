var fs = require('fs');

console.log("Running");

var bytecode = new Uint8Array(fs.readFileSync('implementation_1.wasm'))
WebAssembly.instantiate(bytecode).then(function(module) {
  console.log(module.instance.exports.Add(32, 2)) // outputs 3
});

