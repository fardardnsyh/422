/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./src/**/*.{html,ts}",
  ],
  theme: {
    extend: {
      width: {
        "180px": "180px"
      },
      height: {
        "450px": "450px"
      }
    },
  },
  plugins: [],
}