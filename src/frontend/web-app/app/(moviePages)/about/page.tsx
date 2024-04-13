export default function AboutUsPage() {
    return (
        <div className="container mx-auto px-4 py-8">
            <div className="text-center mb-8">
                <h1 className="text-4xl font-bold text-primary mb-4">
                    Welcome to <span className="text-secondary">Movieasy</span>
                </h1>
                <p className="text-lg font-semibold text-third">
                    Your ultimate destination for movie reviews, trailers, and more!
                </p>
            </div>

            <div className="text-left mb-8 font-semibold text-secondary">
                <h2 className="text-2xl font-bold mb-4 text-secondary">About Movieasy</h2>
                <p className="text-lg mb-4">
                    Movieasy is your one-stop-shop for all things movies. Whether you're a casual moviegoer
                    or a hardcore cinephile, we've got something for everyone. Our platform offers comprehensive
                    movie information, including detailed descriptions, actor profiles, trailers, ratings, and reviews.
                </p>
                <p className="text-lg mb-4">
                    Our community of reviewers is committed to providing you with honest and insightful critiques
                    of the latest releases, helping you make informed decisions about what to watch next.
                    At Movieasy, we believe that watching movies should be easy and enjoyable, and we're here to
                    make that happen.
                </p>
            </div>

            <div className="text-left">
            <h2 className="text-2xl font-bold mb-4 text-secondary">FAQ</h2>
                <ol className="list-decimal font-semibold text-primary list-inside">
                    <li className="text-lg text-secondary mb-4">
                        <strong className="text-third" >What genres of movies do you cover?</strong><br />
                        We cover a wide range of genres, including action, comedy, drama, horror, sci-fi, and more.
                        Whatever your taste in movies, you're sure to find something you love on Movieasy.
                    </li>
                    <li className="text-lg text-secondary mb-4">
                        <strong className="text-third">How often are movie reviews updated?</strong><br />
                        Our team works hard to provide timely reviews of the latest releases. Reviews are typically
                        updated on a daily basis to ensure that you have access to the most up-to-date information.
                    </li>
                    <li className="text-lg text-secondary mb-4">
                        <strong className="text-third">Can I submit my own movie reviews?</strong><br />
                        Absolutely! We encourage our users to share their thoughts and opinions on movies.
                        Simply create an account on Movieasy and start submitting your reviews today.
                    </li>
                </ol>
            </div>
        </div>
    );
}