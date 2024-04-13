import React from 'react';

export default function TermsOfServicePage() {
    return (
        <div className="container mx-auto px-4 py-8">
            <h1 className="text-3xl font-bold mb-8 text-secondary">Terms of Service</h1>

            <div className="text-lg mb-8 text-third font-semibold">
                <p>Welcome to <span className="text-secondary font-bold">Movieasy</span>! These terms and conditions outline the rules and regulations for the use of Movieasy's Website, located at www.movieasy.xyz.</p>
                <p className="mt-4">By accessing this website, we assume you accept these terms and conditions. Do not continue to use <span className="text-secondary font-bold">Movieasy</span> if you do not agree to take all of the terms and conditions stated on this page.</p>
            </div>

            <div className="text-lg mb-8">
                <h2 className="text-2xl text-secondary font-bold mb-4">Cookies</h2>
                <p className='text-third font-semibold'>Movieasy uses cookies to enhance the user experience. By accessing Movieasy, you agree to our use of cookies in accordance with EU cookie law requirements.</p>
            </div>

            <div className="text-lg mb-8">q
                <h2 className="text-2xl text-secondary font-bold mb-4">Movie Information</h2>
                <p className='text-third font-semibold'>Movieasy provides movie information such as covers, titles, descriptions, and trailers for informational and critical purposes only. The movie information displayed on Movieasy is sourced from publicly available APIs and databases. Movieasy does not claim ownership of any movie information displayed on the website, and it is provided solely for the purpose of enhancing the user experience and enabling movie criticism. The use of movie information on Movieasy is compliant with applicable copyright laws.</p>
            </div>

            <div className="text-lg mb-8">
                <h2 className="text-2xl text-secondary font-bold mb-4">License</h2>
                <p className='text-third font-semibold'>Unless otherwise stated, Movieasy does not own the movies or any rights to them. All movie information displayed on Movieasy belongs to their respective copyright holders. You may access this information on Movieasy for your own personal use, but you are not allowed to use it for commercial purposes or distribution without proper authorization from the respective copyright holders.</p>
            </div>
        </div>
    );
}
