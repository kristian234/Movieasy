'use client'

import { Movie, PagedResult } from '@/types';
import React from "react";
import MovieCard from './movie-card';
import { Fragment } from 'react';
import Carousel from 'react-multi-carousel';
import 'react-multi-carousel/lib/styles.css';

interface Props {
    movies: PagedResult<Movie>
}

const responsive = {
    desktop: {
        breakpoint: { max: 3000, min: 1024 },
        items: 6,
        slidesToSlide: 2 // optional, default to 1.
    },
    tablet: {
        breakpoint: { max: 1024, min: 464 },
        items: 3,
        slidesToSlide: 2 // optional, default to 1.
    },
    mobile: {
        breakpoint: { max: 464, min: 0 },
        items: 1,
        slidesToSlide: 1 // optional, default to 1.
    }
}

export default function MovieCarousel({ movies }: Props) {
    const urlm = "https://cdn.pixabay.com/photo/2023/11/09/19/36/zoo-8378189_1280.jpg";
    return (
        <Fragment>
            <div className="p-4carousel">
                <Carousel
                    responsive={responsive}
                    additionalTransfrom={0}
                    arrows
                    autoPlaySpeed={3000}
                    centerMode={true}
                    className=""
                    containerClass="container-with-dots"
                    dotListClass=""
                    draggable
                    focusOnSelect={false}
                    infinite
                    itemClass="carousel-item-padding-50-px"
                    keyBoardControl
                    minimumTouchDrag={80}
                    pauseOnHover
                    renderArrowsWhenDisabled={false}
                    renderButtonGroupOutside={false}
                    renderDotsOutside={false}
                    rewind={false}
                    rewindWithAnimation={false}
                    shouldResetAutoplay
                    showDots={false}
                    sliderClass=""

                    swipeable>
                    {movies.items?.map((movie, index) => (
                        <div className='carousel-item'>
                            <MovieCard
                                isCarousel={true}
                                key={index}
                                title={movie.title}
                                description={movie.description}
                                imageUrl={urlm} />
                        </div>
                    ))}
                </Carousel>
            </div>
        </Fragment >
    )
}