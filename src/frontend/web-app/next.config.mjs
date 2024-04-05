/** @type {import('next').NextConfig} */
const nextConfig = {
    output: 'standalone',
    eslint:{
        ignoreDuringBuilds: true
    },
    typescript:{
        ignoreBuildErrors: true
    },
    images:{
        domains:[
            'cdn.pixabay.com',
            'res.cloudinary.com'
        ]
    }
};

export default nextConfig;
