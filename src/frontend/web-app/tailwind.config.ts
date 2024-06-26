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
        darkHeader: '#0b0b17',
        body: '#080212',
        danger: '#D74338',
        superdanger: '#C91D12'
      },
      boxShadow:{
        '3xl': '0 35px 60px -15px rgba(0, 0, 0, 0.3)',
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
