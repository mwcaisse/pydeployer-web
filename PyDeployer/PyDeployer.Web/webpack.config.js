"use strict";

const { VueLoaderPlugin } = require("vue-loader");
const path = require("path");


module.exports = {
    entry: "./web-src/main.js",
    output: {
        path: path.resolve(__dirname, "wwwroot"),
        filename: "main-bundle.js"
    },
    module: {
        rules: [
            { test: /\.vue$/, use: "vue-loader" }
        ]
    },
    plugins: [
        new VueLoaderPlugin()
    ]
};