using CatForum.Context;
using CatForum.Interfaces;
using CatForum.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CatForum.Repositories
{
    public class PostDetailRepository : IPostDetailRepository
    {
        private CatContext db { get; set; }
        public PostDetailRepository()
        {
            db = new CatContext();
        }
        public PostDetailRepository(CatContext db)
        {
            this.db = db;
        }
        public IEnumerable<PostDetail> SelectAll()
        {
            return db.PostDetails.ToList();
        }
        public IEnumerable<PostDetail> SelectByProvince(int id)
        {
            return db.PostDetails.Where(p => p.Address.ProvinceId == id && p.Status != 2).ToList();
        }
        public IEnumerable<PostDetail> SelectByType(int id)
        {
            return db.PostDetails.Where(p => p.TypeId == id && p.Status != 2).ToList();
        }
        public IEnumerable<PostDetail> SelectByOwnerOnType(int ownerid, int typeId)
        {
            return db.PostDetails.Where(p => p.TypeId == typeId && p.Post.UserId == ownerid && p.Status != 2).ToList();
        }
        public IEnumerable<PostDetail> SelectDescDate(int? type, int? offset)
        {
            if (offset == null || offset == 0)
                offset = 0;
            else
                offset = ((offset - 1) * 10) + 1;
            if (type != null)
                return db.PostDetails.Where(p => p.TypeId == type && p.Status != 2).OrderBy(p => p.Post.Updated).Skip((int)offset).Take(10).ToList();
            else
                return db.PostDetails.Where(p => p.Status != 2).OrderBy(p => p.Post.Updated).Skip((int)offset).Take(10).ToList();
        }
        public IEnumerable<PostDetail> Search(Nullable<int> Type, Nullable<int> Offset, Nullable<int> Eyes, Nullable<int> Coat, Nullable<int> Pattern, Nullable<int> Tail, Nullable<int> Breed, Nullable<int> Province, Nullable<int> Amphur, Nullable<int> Tumbon)
        {
            var posts = db.PostDetails.Where(p => p.TypeId == Type && p.Status != 2);
            if (Eyes != null)
            {
                posts = posts.Where(p => p.Cat.EyesId == Eyes);
            }
            if (Coat != null)
            {
                posts = posts.Where(p => p.Cat.CoatId == Coat);
            }
            if (Pattern != null)
            {
                posts = posts.Where(p => p.Cat.PatternId == Pattern);
            }
            if (Tail != null)
            {
                posts = posts.Where(p => p.Cat.TailId == Tail);
            }
            if (Province != null)
            {
                posts = posts.Where(p => p.Address.ProvinceId == Province);
            }
            if (Amphur != null)
            {
                posts = posts.Where(p => p.Address.AmphurId == Amphur);
            }
            if (Tumbon != null)
            {
                posts = posts.Where(p => p.Address.TumbonId == Tumbon);
            }
            if (Offset != null)
            {
                Offset = ((Offset - 1) * 10) + 1;
                posts = posts.Skip((int)Offset);
            }
            return posts.OrderBy(p => p.Post.Updated).Take(10).ToList();
        }
        public PostDetail SelectById(int id)
        {
            return db.PostDetails.Find(id);
        }
        public void Add(PostDetail obj)
        {
            db.PostDetails.Add(obj);
        }

        public void Update(PostDetail obj)
        {
            db.Entry(obj).State = EntityState.Modified;
        }

        public void Delete(int? id)
        {
            PostDetail post = db.PostDetails.Find(id);
            db.PostDetails.Remove(post);
        }

        public void Save()
        {
            db.SaveChanges();
        }
    }
}