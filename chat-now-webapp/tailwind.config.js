/** @type {import('tailwindcss').Config} */
const withMt = require("@material-tailwind/react/utils/withMT");

export default withMt({
  content: ["./src/**/*.{jsx,js}", "./index.html"],
  theme: {
    extend: {
      fontFamily: {
        Nunito: ["Nunito", "sans-serif"],
        NotoThai: ["Noto-Thai", "sans-serif"],
        NotoKannada: ["Noto-Kannada", "serif"],
      },
      colors: {
        "dark-purple": "#42409B",
        purple: "#615EFC",
        "light-purple": "#7E8EF1",
        beige: "#D1D8C5",
        "light-beige": "#EEEEEE",
      },
    },
  },
  plugins: [],
});
