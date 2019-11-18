using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using DomesticViolencePortal.Models;

namespace DomesticViolencePortal.DAL
{
    public class DomesticViolenceInitializer : DropCreateDatabaseAlways<DomesticViolenceContext>
    {
      

        protected override void Seed(DomesticViolenceContext context)
        {

            context.Roles.Add(new Role { RoleName = "Admin" });
            context.Roles.Add(new Role { RoleName = "Guest" });

            context.Logins.Add(
                new Login
                {
                    UserLogin = "Admin",
                    Password = new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(System.Text.Encoding.Unicode.GetBytes("12"))
                });

            context.SaveChanges();


            context.Logins.Add(
                new Login
                {
                    UserLogin = "User",
                    Password = new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(System.Text.Encoding.Unicode.GetBytes("1"))
                });

            context.SaveChanges();


            context.Logins.Add(
                new Login
                {
                    UserLogin = "Jane",
                    Password = new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(System.Text.Encoding.Unicode.GetBytes("2"))
                });

            context.SaveChanges();


            Donation donation = new Donation
            {
                LastName = "Степаненко",
                FirstName = "Светлана",              
                Email = "user2@gmail.com",
                Theme = "Могу помочь медикаментами",
                Title = "Могу отдать инсулин",
                Text = "Могу отдать три пачки инсулина. Мои контакты :Почта:ivanov@gmail.com, телефон:829377979, Обращайтесь",
                Image = @"/Content/images/person_7.jpg",
                Date = DateTime.Now

            };


          
            Donation donation1 = new Donation
            {

                FirstName = "Виктория ",               
                LastName = "Петрова",
                Email = "user1@gmail.com",
                Theme = "Могу помочь  с детскими теплыми вещами",
                Title = "Есть вещи на девочку 2 года",
                Text = "Могу отдать комплекты зимних и летних детских вещей. Мои контакты :Почта:Sidorova@gmail.com, телефон:23423424, звоните или пишите",
                Image = @"/Content/images/person_5.jpg",
                Date = DateTime.Now

            };

            Donation donation2 = new Donation
            {
                FirstName = "Иван",
                LastName = "Иванов",
                Email = "user@gmail.com",
                Theme = "Могу предоставить временное жилье на сутки",
                Title = "Временное убежище на сутки",
                Text = "Есть свободная квартира, могу обеспечить вам и вашим детям убежище на сутки.Бесплатно. Мои контакты :Почта:kucenko@gmail.com, телефон:4525245245225, звоните или пишите",
                Image = @"/Content/images/person_2.jpg",
                Date = DateTime.Now

            };

            context.Donations.Add(donation);
            context.Donations.Add(donation1);
            context.Donations.Add(donation2);
            context.SaveChanges();

         




            User user = new User
            {   
                LastName = "Иванов",
                FirstName = "Иван",
                Image = @"/Content/images/person_2.jpg",
                Email = "user@gmail.com",
                LoginId = 1,
                RoleId = 1
            };

            User user1 = new User
            {
                LastName = "Петрова",
                FirstName = "Виктория",
                Image = @"/Content/images/person_5.jpg",
                Email = "user1@gmail.com",
                LoginId = 2,
                RoleId = 2
            };

            User user2 = new User
            {
                LastName = "Степаненко",
                FirstName = "Светлана",
                Image = @"/Content/images/person_7.jpg",
                Email = "user2@gmail.com",
                LoginId = 3,
                RoleId = 2
            };
            context.Users.Add(user);
            context.Users.Add(user1);
            context.Users.Add(user2);           
            context.SaveChanges();


            NeedHelpUser needHelpUser = new NeedHelpUser
            {
                UserId = 1,
                FirstName = "Иван",
                LastName = "Иванов",
                Email = "user@gmail.com",
                Theme = "Нужна помощь в сборе средств для женщины избитой мужем",
                Title = "Сбор средств",
                Text = "Женщина в больнице, срочно нужна врачебная помощь. Мои контакты для деталей:Почта:user@gmail.com, телефон:4525245245225, картра для приема средств на лечение : 2444678934561234",
                Image = @"/Content/images/person_2.jpg",
                Date = DateTime.Now

            };

            NeedHelpUser needHelpUser1 = new NeedHelpUser
            {

                UserId = 2,
                LastName = "Петрова",
                FirstName = "Виктория",
                Email = "user1@gmail.com",
                Theme = "Нужна помощь ",
                Title = "Нужна помощь юриста",
                Text = "Помогите пожалуйста с бесплатной консультацией юриста, по поводу родительстких прав.Нет денег обратиться к платному Напишите мне пожалуйста или позвоните :Почта:user@gmail.com, телефон:2342423424234",
                Image = @"/Content/images/person_5.jpg",
                Date = DateTime.Now

            };

            NeedHelpUser needHelpUser2 = new NeedHelpUser
            {

                UserId = 3,
                LastName = "Степаненко",
                FirstName = "Светлана",
                Email = "user2@gmail.com",
                Theme = "Нужна помощь!",
                Title = "Прошу о временной няне на пару дней",
                Text = "У меня грудной ребенок, муж нас выгоняет из дома.Может кто взять ребенка днем к себе на дом пока я ищу квартиру?.Родители в другом городе(помочь некому. Позвоните мне на  телефон, если можете помочь :324243423423424 .Заранее спасибо!",
                Image = @"/Content/images/person_7.jpg",
                Date = DateTime.Now

            };

            context.NeedHelpUsers.Add(needHelpUser);
            context.NeedHelpUsers.Add(needHelpUser1);
            context.NeedHelpUsers.Add(needHelpUser2);
            context.SaveChanges();



            Volonteer volonteer = new Volonteer
            {
                
                FirstName = "Николай Гончаренко",
                Email = "goncharenko@gmail.com",
                Message = "Здравствуйте, я бы хотел стать волонтером в  вашем проекте.Что для этого нужно?"
               
            };

            Volonteer volonteer1 = new Volonteer
            {

                FirstName = "Виктор Клименко",
                Email = "klimenko@gmail.com",
                Message = "Здравствуйте, я бы хотел стать волонтером .Могу поставлять детские вещи- у меня дети и много ненужных более вещей"

            };

            Volonteer volonteer2 = new Volonteer
            {

                FirstName = "Сергей Задорин",
                Email = "Zadorin@gmail.com",
                Message = "Здравствуйте, я бы хотел стать волонтером.Я юрист и мог бы оказывать юридическую помощь"

            };

            context.Volonteers.Add(volonteer1);
            context.Volonteers.Add(volonteer1);
            context.Volonteers.Add(volonteer2);
            context.SaveChanges();

            Question question = new Question
            {

                FirstName = "Татьяна Семенова",
                Title = "Консультация",
                Email = "semenove@gmail.com",
                Message = "Здравствуйте,можно к вам записаться на прием к бесплатному юристу? "

            };

            Question question1 = new Question
            {

                FirstName = "Снежана Николаева",
                Title = "Консультация юриста",
                Email = "nikolaeva@gmail.com",
                Message = "Здравствуйте,можно к вам записаться на прием к бесплатному юристу или помощь возможна только онлайн? "

            };

            Question question2 = new Question
            {

                FirstName = "Татьяна Семенова",
                Title = "Группы поддержки",
                Email = "semenova@gmail.com",
                Message = "Здравствуйте,вы планируете устраивать встречи для группы поддержки? "

            };

            Question question3 = new Question
            {

                FirstName = "Ульяна Степаненко",
                Title = "Помощь в других городах",
                Email = "stepanenko@gmail.com",
                Message = "Здравствуйте,вы планируете расширять свою работу на другие города Украины? "

            };
            context.Questions.Add(question1);
            context.Questions.Add(question2);
            context.Questions.Add(question3);
            context.Questions.Add(question);
            context.SaveChanges();
           
            
            Post firstPost = new Post
            {
                PostId = 1,
                Title = "Виды насилия в семье",
                Date = new DateTime(2019, 08, 08),
                Text = "Физическое – умышленное нанесение одним членом семьи другому члену семьи побоев, телесных повреждений, что может привести или привело к смерти пострадавшего, нарушения физического или психического здоровья, нанесению ущерба его чести и достоинства." +
                 "Сексуальное – противоправное посягательство одного члена семьи на половую неприкосновенность другого члена семьи,а также действия сексуального характера по отношению к несовершеннолетнемучлену семьи.            Психологическое – насилие связанное с действием одного члена семьи на психику другого члена семьи путем словесных оскорблений или угроз, преследования,запугивания,которыми преднамеренно вызывается эмоциональная неуверенность, неспособность защитить себя и может наноситься или наносится вред психическому здоровью.         Экономическое – умышленное лишение одним членом семьи другого члена семьи жилья,еды, одежды и другого имущества или средств, на которые потерпевший имеет предусмотренное законом право,что может привести к его смерти,        вызвать нарушение физического или психического здоровья."
              ,
                UserId = 2
            };
            Post secondPost = new Post
            {
                PostId = 2,
                Title = "Как поддержать и не сделать хуже?", Date = new DateTime(2019, 06, 06),
                Text ="Небольшая, но искренняя поддержка лучше её симуляции. Часто на жалобы реагируют токсичными способами именно оттого, что у собеседника, от которого ждут поддержки, нет на неё сил или ресурса, но он боится признаться в этом. Не нужно стыдиться: совершенно нормально не хотеть или не мочь поддерживать кого-то. " +
                      "Предлагайте только то, что действительно сможете предоставить без насилия над собой. Возможно, вы сейчас способны послушать собеседника только пять минут, и не больше. Или можете поговорить полчаса, но не готовы оказывать практическую помощь.      " +
                      " Если у вас нет сил даже на то, чтобы побыть рядом с человеком, когда он испытывает трудные чувства             честнее всего будет сказать об этом: «Прости, пожалуйста, но я сейчас очень устал, на нервах, совершенно вымотан.Я мог бы поговорить с тобой завтра, если тебе это будет удобно». Не факт, что собеседник на вас не обидится, — но это лучше, чем совершать над собой насилие, а потом сливать агрессию на другого.",
                UserId = 2
               
            };

            Post thirdPost = new Post
            {
                PostId = 3,
                Title = "Море и эмоциональная перезагрузка",
                Date = new DateTime(2019, 06, 06),
                Text = "Море – отдых для тела и души.Кроме того,морской воздух и вода очень полезны для человеческого организма.В чем же их польза ? Приезжая к морю, мы попадаем в другой мир.Шорох песк под ногами,перекаты гальки,               крики чаек,плеск волн… " +
                       "Мало где можно ощутить столь полное единство с природой, как на море.Его безграничное величие и красота,изменчивый цвет и стихийная мощь дарят нам целую гамму неизведанных чувств." +
                       " Море – не только отдых для тела и души: это отличный способ прислушаться к себе и выбрать новый путь к другой жизни, а главное-решиться на первый шаг.Который самый трудный",
                UserId = 3
            };

            firstPost.Image = @"/Content/images/thirdpost.jpg";
            secondPost.Image = @"/Content/images/firstPost.jpg";
            thirdPost.Image = @"/Content/images/secondpost.jpg";

            context.Posts.Add(firstPost);
            context.Posts.Add(secondPost);
            context.Posts.Add(thirdPost);
            context.SaveChanges();

            Comment firstComment = new Comment {Posts = firstPost, UserId = 2,Title = "Мы держимся", Date = new DateTime(2019, 07, 07), CommentId = 1, PostId = 1, Text = "Спасибо за поддержку!Ваши посты очень информативны." };
            Comment secondComment = new Comment { Posts = secondPost ,UserId = 1, Title = "Это очень важная тема", Date = new DateTime(2019, 09, 04), CommentId = 2, PostId = 2, Text = "У меня есть близкие , которых нужно поддерживать, но иногда на это нет сил.Ваша статья помогла мне разобраться, как взаимодействовать в эти моменты.Спасибо!" };

            Comment thirdComment = new Comment { Posts = thirdPost,UserId = 2, Title = "Едем на море-срочно!", Date = new DateTime(2019, 07, 07), CommentId = 3, PostId = 3, Text = "Море всегда помогает мне сделать перезагрузку сознания.Это хорошее время спланировать будущее и вернуться домой со свежими решениями и идеями" };

            firstComment.Image = @"/Content/images/person_8.jpg";
      
            secondComment.Image = @"/Content/images/person_7.jpg";

            thirdComment.Image = @"/Content/images/person_5.jpg";

            context.Comments.Add(firstComment);
            context.Comments.Add(secondComment);
            context.Comments.Add(thirdComment);
            context.SaveChanges();

            base.Seed(context);
        }
    }
}