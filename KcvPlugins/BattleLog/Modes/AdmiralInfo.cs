using Grabacr07.KanColleWrapper;
using Grabacr07.KanColleWrapper.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMing.Logger.Modes
{
    public class AdmiralInfo
    {
        public Guid Id { get; set; }
        public DateTime CreateDate { get; set; }

        public string MemberId { get; set; }

        public string Nickname { get; set; }

        public string Comment { get; set; }

        /// <summary>
        /// 提督経験値を取得します。
        /// </summary>
        public int Experience { get; set; }

        /// <summary>
        /// 次のレベルに上がるために必要な提督経験値を取得します。
        /// </summary>
        public int ExperienceForNexeLevel { get; set; }

        /// <summary>
        /// 艦隊司令部レベルを取得します。
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 提督のランク名 (元帥, 大将, 中将, ...) を取得します。
        /// </summary>
        public string Rank { get; set; }

        /// <summary>
        /// 出撃時の勝利数を取得します。
        /// </summary>
        public int SortieWins { get; set; }

        /// <summary>
        /// 出撃時の敗北数を取得します。
        /// </summary>
        public int SortieLoses { get; set; }

        /// <summary>
        /// 出撃時の勝率を取得します。
        /// </summary>
        public double SortieWinningRate { get; set; }

        /// <summary>
        /// 司令部に所属できる艦娘の最大値を取得します。
        /// </summary>
        public int MaxShipCount { get; set; }

        /// <summary>
        /// 司令部が保有できる装備アイテムの最大値を取得します。
        /// </summary>
        public int MaxSlotItemCount { get; set; }

        /// <summary>
        /// 所有している燃料数を取得します。
        /// </summary>
        public int Fuel { get; set; }


        /// <summary>
        /// 所有している弾薬数を取得します。
        /// </summary>
        public int Ammunition { get; set; }

        /// <summary>
        /// 所有している鉄鋼数を取得します。
        /// </summary>
        public int Steel { get; set; }

        /// <summary>
        /// 所有しているボーキサイト数を取得します。
        /// </summary>
        public int Bauxite { get; set; }

        /// <summary>
        /// 所有している開発資材の数を取得します。
        /// </summary>
        public int DevelopmentMaterials { get; set; }

        /// <summary>
        /// 所有している高速修復材の数を取得します。
        /// </summary>
        public int InstantRepairMaterials { get; set; }

        /// <summary>
        /// バケツ！！！ ぶっかけ！！！！
        /// </summary>
        public int Bucket { get; set; }

        /// <summary>
        /// 所有している高速建造材の数を取得します。
        /// </summary>
        public int InstantBuildMaterials { get; set; }

        public AdmiralInfo() { }

        public AdmiralInfo(Admiral admiral, Materials materials)
        {
            this.MemberId = admiral.MemberId;
            this.Nickname = admiral.Nickname;
            this.Comment = admiral.Comment;
            this.Experience = admiral.Experience;
            this.ExperienceForNexeLevel = admiral.ExperienceForNexeLevel;
            this.Level = admiral.Level;
            this.Rank = admiral.Rank;
            this.SortieWins = admiral.SortieWins;
            this.SortieLoses = admiral.SortieLoses;
            this.SortieWinningRate = admiral.SortieWinningRate;
            this.MaxShipCount = admiral.MaxShipCount;
            this.MaxSlotItemCount = admiral.MaxSlotItemCount;

            this.Fuel = materials.Fuel;
            this.Ammunition = materials.Ammunition;
            this.Steel = materials.Steel;
            this.Bauxite = materials.Bauxite;
            this.DevelopmentMaterials = materials.DevelopmentMaterials;
            this.InstantRepairMaterials = materials.InstantRepairMaterials;
            this.Bucket = materials.Bucket;
            this.InstantBuildMaterials = materials.InstantBuildMaterials;

            this.Id = Guid.NewGuid();
            this.CreateDate = DateTime.Now;
        }
    }
}
