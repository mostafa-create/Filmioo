\documentclass[12pt,a4paper]{article}

% --- Packages ---
\usepackage[utf8]{inputenc}
\usepackage[english]{babel}
\usepackage{hyperref} % For clickable links
\usepackage{graphicx} % For images
\usepackage{xcolor}   % For colored text
\usepackage{geometry} % Page margins
\usepackage{enumitem} % Better lists
\usepackage{listings} % For code snippets

\geometry{margin=1in}

% --- Custom Colors ---
\definecolor{cosmicpurple}{HTML}{9333EA}
\definecolor{codegray}{rgb}{0.5,0.5,0.5}

\hypersetup{
    colorlinks=true,
    linkcolor=black,
    urlcolor=cosmicpurple,
    pdftitle={Filmioo Project Documentation}
}

% --- Title Info ---
\title{
    \Huge \textbf{\color{cosmicpurple} Filmioo} \\
    \large Cinematic Movie Discovery \& Review Platform
}
\author{\textbf{Mostafa Yassin} \\ Software Engineering Student | Sohag University}
\date{April 2026}

\begin{document}

\maketitle

\section{Introduction}
\textbf{Filmioo} is a high-end web application developed using the \textbf{ASP.NET Core MVC} framework. It is designed to provide users with an immersive, cinematic experience while browsing movies, managing personal watchlists, and sharing reviews. The project highlights a modern "Cosmic" dark-themed UI and follows professional software architecture standards.

\section{Key Features}
\begin{itemize}[label=\textcolor{cosmicpurple}{\textbullet}]
    \item \textbf{Real-Time Interactions:} Leverages \textbf{AJAX \& jQuery} to perform CRUD operations on reviews (Add, Edit, Delete) without page reloads.
    \item \textbf{Clean Architecture:} Implemented using a \textbf{3-Layer Architecture} consisting of Presentation, Business Logic, and Data Access layers.
    \item \textbf{Design Patterns:} Utilizes the \textbf{Repository Pattern} and \textbf{Unit of Work} for efficient and maintainable data management.
    \item \textbf{Cosmic UI/UX:} A custom responsive interface built with a hybrid of \textbf{Tailwind CSS} and \textbf{Bootstrap 5}, featuring glassmorphism and Animate On Scroll (AOS) effects.
    \item \textbf{Identity \& Security:} Fully integrated with \textbf{ASP.NET Core Identity} for secure authentication and user-specific data management.
\end{itemize}

\section{Tech Stack}
\subsection{Backend}
\begin{itemize}
    \item Framework: ASP.NET Core MVC 8.0
    \item Language: C\#
    \item ORM: Entity Framework Core (Code First)
    \item Database: Microsoft SQL Server
\end{itemize}

\subsection{Frontend}
\begin{itemize}
    \item Styling: Tailwind CSS \& Bootstrap 5
    \item Scripts: JavaScript (ES6+), jQuery, AJAX
    \item Animations: AOS.js
\end{itemize}

\section{Project Structure}
The solution is organized into three distinct layers to ensure separation of concerns:
\begin{enumerate}
    \item \textbf{Demo.PL (Presentation Layer):} Handles the UI, Razor Views, and ViewModels.
    \item \textbf{Demo.BLL (Business Logic Layer):} Contains repository interfaces and service implementations.
    \item \textbf{Demo.DAL (Data Access Layer):} Manages the Database Context, Migrations, and Entities.
\end{enumerate}

\section{Installation \& Setup}
To run this project locally, follow these steps:
\begin{lstlisting}[language=bash, basicstyle=\small\ttfamily, breaklines=true, frame=single]
# 1. Clone the repo
git clone https://github.com/mostafa-create/Filmioo.git

# 2. Apply Migrations
dotnet ef database update

# 3. Run the application
dotnet run
\end{lstlisting}

\section{Project Demo}
A full video demonstration of the platform, including the dynamic review system and profile management, can be found on LinkedIn:

\vspace{0.5cm}
\centering
\textbf{\url{INSERT_YOUR_LINKEDIN_VIDEO_URL_HERE}}

\end{document}
