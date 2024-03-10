import type { Config } from "tailwindcss";

const config: Config = {
  content: [
    "./pages/**/*.{js,ts,jsx,tsx,mdx}",
    "./components/**/*.{js,ts,jsx,tsx,mdx}",
    "./app/**/*.{js,ts,jsx,tsx,mdx}",
    'node_modules/flowbite-react/lib/esm/**/*.js',
  ],
  theme: {
    extend: {
      colors: {
        primary: '#ffffff',
        secondary: '#52527A',
        third: '#676790',
        header: '#0D0D1A',
        body: '#080212',
      },
      screens: {
        'ssm': '420px'
      }
    },
  },
  corePlugins: {
    aspectRatio: false
  },
  plugins: [
    require('@tailwindcss/aspect-ratio'),
    require('flowbite/plugin'),
    require('@tailwindcss/forms')({
      strategy: 'class',
    }),
  ],
};
export default config;
