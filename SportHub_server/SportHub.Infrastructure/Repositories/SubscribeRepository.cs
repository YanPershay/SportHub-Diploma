﻿using Microsoft.EntityFrameworkCore;
using SportHub.Core.Entities;
using SportHub.Core.Repositories;
using SportHub.Infrastructure.Data;
using SportHub.Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportHub.Infrastructure.Repositories
{
    public class SubscribeRepository : Repository<Subscribe>, ISubscribeRepository
    {
        public SubscribeRepository(SportHubContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Subscribe>> GetSubscribersByUserId(Guid id)
        {
            return await _context.Subscribes.Where(s => s.UserId.Equals(id)).Include(u => u.Subscriber).ThenInclude(u => u.UserInfo).ToListAsync();
        }

        public async Task<IEnumerable<Subscribe>> GetMySubscribesByUserId(Guid subscriberId)
        {
            return await _context.Subscribes.Where(s => s.SubscriberId.Equals(subscriberId)).Include(u => u.User).ThenInclude(u => u.UserInfo).ToListAsync();
        }

        public async Task<int> GetSubscribersCountByUserId(Guid id)
        {
            return await _context.Subscribes.Where(l => l.UserId == id).CountAsync();
        }

        public async Task<int> GetMySubscribesCountByUserId(Guid subscriberId)
        {
            return await _context.Subscribes.Where(l => l.SubscriberId == subscriberId).CountAsync();
        }

        public async Task<SubscriptionsCount> GetSubscriptionsCountByUserId(Guid userId)
        {
            SubscriptionsCount subscriptionsCount = new SubscriptionsCount();
            subscriptionsCount.MySubscribesCount = await _context.Subscribes.Where(l => l.SubscriberId == userId).CountAsync();
            subscriptionsCount.SubscribersCount = await _context.Subscribes.Where(l => l.UserId == userId).CountAsync();
            return subscriptionsCount;
        }

        public async Task<bool> IsUserSubscribed(Guid subscriberId, Guid userId)
        {
            return await _context.Subscribes.AnyAsync(u => u.UserId.Equals(userId) && u.SubscriberId.Equals(subscriberId));
        }

        public async Task<Subscribe> GetSubscribeObject(Guid subscriberId, Guid userId)
        {
            return await _context.Subscribes.FirstOrDefaultAsync(u => u.UserId.Equals(userId) && u.SubscriberId.Equals(subscriberId));
        }
    }
}
