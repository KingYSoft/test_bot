#!/usr/bin/env python3
"""
黄金价格分析工具
用于输入和分析黄金价格，判断是否适合买入
"""

def analyze_gold_price(current_price, historical_prices=None):
    """
    分析黄金价格是否适合买入
    :param current_price: 当前黄金价格（元/克）
    :param historical_prices: 历史价格列表，格式为[(date, price), ...]
    :return: 分析结果字典
    """
    
    analysis = {
        'current_price': current_price,
        'recommendation': '',
        'reasons': [],
        'supporting_data': {}
    }
    
    # 简单的价格区间分析
    if current_price < 500:
        analysis['recommendation'] = '强烈建议买入'
        analysis['reasons'].append('价格处于相对低位')
        analysis['reasons'].append('投资价值较高')
    elif current_price < 530:
        analysis['recommendation'] = '建议买入'
        analysis['reasons'].append('价格处于中低位')
        analysis['reasons'].append('有一定投资价值')
    elif current_price < 560:
        analysis['recommendation'] = '观望'
        analysis['reasons'].append('价格处于中间位置')
        analysis['reasons'].append('可等待更明确信号')
    elif current_price < 590:
        analysis['recommendation'] = '谨慎买入'
        analysis['reasons'].append('价格偏高')
        analysis['reasons'].append('风险收益比一般')
    else:
        analysis['recommendation'] = '不建议买入'
        analysis['reasons'].append('价格过高')
        analysis['reasons'].append('建议等待回调')
    
    # 如果提供了历史数据，进行更多分析
    if historical_prices and len(historical_prices) >= 2:
        # 计算近期趋势
        recent_prices = [p[1] for p in historical_prices[-10:]]  # 最近10天
        avg_recent = sum(recent_prices) / len(recent_prices)
        
        analysis['supporting_data']['recent_avg'] = round(avg_recent, 2)
        
        if current_price > avg_recent:
            analysis['reasons'].append(f'当前价格高于近期均价({avg_recent:.2f})')
        else:
            analysis['reasons'].append(f'当前价格低于近期均价({avg_recent:.2f})')
    
    return analysis


def main():
    print("=== 黄金价格分析工具 ===")
    print("注意：以下价格仅供参考，实际投资请以实时行情为准")
    
    try:
        # 获取用户输入的当前金价
        current_price = float(input("请输入当前黄金价格（元/克）: "))
        
        print("\n分析结果:")
        print("-" * 30)
        
        result = analyze_gold_price(current_price)
        
        print(f"当前价格: {result['current_price']} 元/克")
        print(f"投资建议: {result['recommendation']}")
        print("理由:")
        for reason in result['reasons']:
            print(f"  - {reason}")
        
        if result['supporting_data']:
            print("\n补充信息:")
            for key, value in result['supporting_data'].items():
                print(f"  - {key}: {value}")
                
        print("\n" + "="*50)
        print("风险提示：")
        print("1. 贵金属价格波动较大，请根据自身风险承受能力投资")
        print("2. 本分析仅供参考，不构成投资建议")
        print("3. 投资前请充分了解相关产品特性")
        
    except ValueError:
        print("输入错误，请输入有效的数字")
    except KeyboardInterrupt:
        print("\n程序已退出")


if __name__ == "__main__":
    main()